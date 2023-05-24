import { interval, map, Observable, of } from 'rxjs';
import { delay, shareReplay, switchMap, take, tap } from 'rxjs/operators';
import { DataService } from 'src/app/services/data.service';

import { HttpClient } from '@angular/common/http';
import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { NgbModal, NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';

import { CrossingInfoComponent } from '../crossing-info/crossing-info.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  @ViewChild('crossingChartContent', { static: true })
  crossingChartContent!: TemplateRef<any>;

  private hubConnection!: HubConnection;

  page: number = 1;
  itemsPerPage: number = 10;
  totalPages!: number;
  entryCount: number = 0;

  sortDirection: 'asc' | 'desc' = 'asc';
  sortField: string = 'createdTime';

  searchText: string = '';

  showAlert: boolean = false;
  previousData: any;

  filteredData$: Observable<any>;
  myData$: Observable<any>;

  form: FormGroup;

  constructor(
    private http: HttpClient,

    private offcanvasService: NgbOffcanvas,
    private router: Router,
    private ngbModal: NgbModal,
    private dataService: DataService,
    private formBuilder: FormBuilder
  ) {
    this.myData$ = this.dataService.fetchDataReversed();

    this.filteredData$ = new Observable();

    this.form = this.formBuilder.group({
      searchText: [''],
      selectedDate: new FormControl(''),
    });

    this.sortDirection = 'desc';
    this.sortField = 'createdTime';
  }

  ngOnInit(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7065/notificationHub')
      .build();

    this.hubConnection.on('ReceiveNotification', (message: string) => {
      console.log('Received notification:', message);
    });

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((error) =>
        console.error('Error while starting the connection:', error)
      );

    const pollInterval = 2000;

    this.myData$ = interval(pollInterval).pipe(
      switchMap(() => this.dataService.fetchDataReversed()),
      tap((newData) => {
        console.log('New data:', newData);

        if (JSON.stringify(this.previousData) !== JSON.stringify(newData)) {
          this.showAlert = true;
        }
        this.previousData = newData;
      }),
      delay(5000),
      tap(() => {
        this.showAlert = false;
      }),
      shareReplay(1)
    );

    this.filteredData$ = this.myData$;

    this.filteredData$
      .pipe(
        tap((data) => {
          this.entryCount = data.length;
          this.totalPages = Math.ceil(this.entryCount / this.itemsPerPage);
        })
      )
      .subscribe();

    this.filterTable();
  }

  ngOnDestroy() {
    this.hubConnection.stop();
  }

  openCrossingChart() {
    this.offcanvasService.open(this.crossingChartContent, { position: 'top' });
  }

  setSortField(field: string) {
    if (field === this.sortField) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = field;
      this.sortDirection = 'asc';
    }
    this.filterTable();
  }

  checkTireAlignment(item: any): boolean {
    return item.tires.some((tire: any) => {
      const info = tire.propertySetResponse.wearPatternResponse.info;
      return info === 'linearWearLeft' || info === 'linearWearRight';
    });
  }

  checkTirePressure(item: any): boolean {
    return item.tires.some((tire: any) => {
      const info = tire.propertySetResponse.wearPatternResponse.info;
      return (
        info === 'parabolicWearCentered' || info === 'parabolicWearOutside'
      );
    });
  }

  checkTireRating(item: any): boolean {
    return item.tires.some((tire: any) => {
      const info = tire.regionResponse[0].value;
      return info <= 4;
    });
  }

  getPageRange(): number[] {
    const MAX_PAGE_LINKS = 10;
    const pagesToShow = Math.min(MAX_PAGE_LINKS, this.totalPages);

    const startPage =
      this.page <= Math.ceil(pagesToShow / 2)
        ? 1
        : this.page - Math.floor(pagesToShow / 2);

    const endPage =
      startPage + pagesToShow - 1 > this.totalPages
        ? this.totalPages
        : startPage + pagesToShow - 1;

    return Array.from(
      { length: endPage - startPage + 1 },
      (_, i) => startPage + i
    );
  }

  pageLinkGenerator(pageIndex: number): string {
    return String(pageIndex + 1);
  }

  getDataForCurrentPage(): Observable<any[]> {
    return this.filteredData$.pipe(
      map((data) => {
        const startIndex = (this.page - 1) * this.itemsPerPage;
        const endIndex = startIndex + this.itemsPerPage;
        const sortedData = data.sort((a: any, b: any) => {
          const fieldA = this.getFieldValue(a, this.sortField);
          const fieldB = this.getFieldValue(b, this.sortField);
          if (typeof fieldA === 'string') {
            return (
              fieldA.localeCompare(fieldB) *
              (this.sortDirection === 'asc' ? 1 : -1)
            );
          } else {
            return (fieldA - fieldB) * (this.sortDirection === 'asc' ? 1 : -1);
          }
        });
        return sortedData.slice(startIndex, endIndex);
      })
    );
  }

  getFieldValue(obj: any, path: string): any {
    const parts = path.split('.');
    return parts.reduce((acc, cur) => acc && acc[cur], obj);
  }

  filterTable(): void {
    const searchText = this.form?.get('searchText')?.value ?? '';
    const selectedDate = this.form.get('selectedDate')?.value;

    let filteredData = this.myData$;

    if (searchText) {
      filteredData = filteredData.pipe(
        map((data) =>
          data.filter((item: any) => {
            const searchTextMatches = item.licensePlate.plate
              .toLowerCase()
              .includes(searchText.toLowerCase());
            return searchTextMatches;
          })
        )
      );
    }

    if (selectedDate) {
      filteredData = filteredData.pipe(
        map((data) =>
          data.filter((item: any) => {
            const dateMatches =
              new Date(item.createdTime).toISOString().split('T')[0] ===
              selectedDate;
            return dateMatches;
          })
        )
      );
    }

    this.filteredData$ = filteredData.pipe(
      map((data) => data.slice().reverse()),
      map((data) => {
        return data.sort((a: any, b: any) => {
          const normalizedA =
            ' ' +
            a.licensePlate.plate
              .replace(/å|ä|ö/gi, (matched: string) => matched.toUpperCase())
              .replace(/Å/gi, 'AA')
              .replace(/Ä/gi, 'AE')
              .replace(/Ö/gi, 'OE');

          const normalizedB =
            ' ' +
            b.licensePlate.plate
              .replace(/å|ä|ö/gi, (matched: string) => matched.toUpperCase())
              .replace(/Å/gi, 'AA')
              .replace(/Ä/gi, 'AE')
              .replace(/Ö/gi, 'OE');

          return normalizedA.localeCompare(normalizedB);
        });
      }),
      tap((data) => {
        this.entryCount = data.length;
        this.totalPages = Math.ceil(this.entryCount / this.itemsPerPage);

        if (this.page > this.totalPages) {
          this.page = this.totalPages;
        }
      })
    );
  }

  resetTable(): void {
    this.form.get('searchText')?.setValue('');
    this.form.get('selectedDate')?.setValue('');
    this.page = 1;
    this.filterTable();
  }

  clearSearchField(): void {
    this.form.get('searchText')?.setValue('');
    this.filterTable();
  }

  goToCrossingInfo() {
    this.router.navigate(['/crossing-info']);
  }

  onButtonClick(item: any) {
    const modalRef = this.ngbModal.open(CrossingInfoComponent, { size: 'lg' });
    modalRef.componentInstance.item = item;
  }

  statusInfo: {
    [key: string]: { icon: string; message: string; colorClass: string };
  } = {
    NEUTRAL: {
      icon: 'bi-check-circle-fill',
      message: 'neutral',
      colorClass: 'text-secondary',
    },
    GOOD: {
      icon: 'bi-check-circle-fill',
      message: 'good',
      colorClass: 'text-success',
    },
    MARGINAL: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'marginal',
      colorClass: 'text-warning',
    },
    REPLACE: {
      icon: 'bi-exclamation-triangle-fill',
      message: 'replace',
      colorClass: 'text-danger',
    },
  };

  formatCompactDate(dateString: string): string {
    const formatOptions: Intl.DateTimeFormatOptions = {
      year: 'numeric',
      month: 'short',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      timeZone: 'Europe/Stockholm',
    };
    const locale = 'sv-SE';
    const date = new Date(dateString);
    return new Intl.DateTimeFormat(locale, formatOptions).format(date);
  }
}
