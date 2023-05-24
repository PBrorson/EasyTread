import { BehaviorSubject, Observable, Subject, timer } from 'rxjs';
import { map, switchMap, take, takeUntil, tap } from 'rxjs/operators';
import { DataService } from 'src/app/services/data.service';
import { HubConnectionService } from 'src/app/services/hub-connection.service';

import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { CrossingChartComponent } from '../../views/crossing-chart/crossing-chart.component';
import { CrossingInfoComponent } from '../../views/crossing-info/crossing-info.component';

@Component({
  selector: 'app-crossing-table',
  templateUrl: './crossing-table.component.html',
})
export class CrossingTableComponent implements OnInit, OnDestroy {
  @Input() resetAlerts = false;

  private dataSubject = new BehaviorSubject<any[]>([]);
  private filteredDataSubject = new BehaviorSubject<any[]>([]);
  data$ = this.filteredDataSubject.asObservable();

  private ngUnsubscribe = new Subject<void>();

  filteredData: any[] = [];

  latestLicensePlate = '';
  searchText = '';

  sortKey: string | null = null;
  sortOrder = 'desc';

  selectedDateRange!: Date[];

  showAlert = false; // tillfälligt på

  currentPage = 1;
  entryCount = 0;
  pageSize = 10;
  totalItems = 0;

  constructor(
    private dataService: DataService,
    private hubConnectionService: HubConnectionService,
    private ngbModal: NgbModal
  ) {}

  ngOnInit(): void {
    this.dataService
      .fetchDataReversed()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((data) => {
        this.dataSubject.next(data);
        this.totalItems = data.length;
      });

    this.dataSubject
      .pipe(
        takeUntil(this.ngUnsubscribe),
        map((data) => this.filterData(data))
      )
      .subscribe((filteredData) => this.filteredDataSubject.next(filteredData));

    this.hubConnectionService.notification$
      .pipe(
        takeUntil(this.ngUnsubscribe),
        switchMap(() => {
          console.log('Received notification, fetching new data...');
          return this.dataService.fetchDataReversed().pipe(take(1));
        }),
        tap((newData) => {
          console.log('New data fetched:', newData);
          this.dataSubject.next(newData);
          this.latestLicensePlate = newData[0].licensePlate.plate;
          this.showAlert = true;

          timer(5000).subscribe(() => {
            this.showAlert = false;
          });
        })
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  onSelectedDateRangeChange(newDateRange: Date[]): void {
    this.selectedDateRange = newDateRange;
    this.updateFilteredData();
  }

  updateFilteredData(): void {
    const filteredData = this.filterData(this.dataSubject.value);
    this.filteredDataSubject.next(filteredData);
    this.totalItems = filteredData.length;
  }

  filterData(data: any[]): any[] {
    let filteredData = data;

    if (this.searchText) {
      filteredData = filteredData.filter((item) =>
        item.licensePlate?.plate
          .toLowerCase()
          .includes(this.searchText.toLowerCase())
      );
    }

    if (this.selectedDateRange && this.selectedDateRange.length === 2) {
      const startDate = this.selectedDateRange[0].setHours(0, 0, 0, 0);
      const endDate = this.selectedDateRange[1].setHours(23, 59, 59, 999);

      filteredData = filteredData.filter((item) => {
        const itemDate = new Date(item.createdTime).getTime();

        return itemDate >= startDate && itemDate <= endDate;
      });
    }

    filteredData.sort((a, b) => {
      if (this.sortKey === 'createdTime') {
        const aTime = new Date(a.createdTime).getTime();
        const bTime = new Date(b.createdTime).getTime();
        return this.sortOrder === 'desc' ? bTime - aTime : aTime - bTime;
      } else if (this.sortKey === 'licensePlate') {
        const aPlate = a.licensePlate?.plate || '';
        const bPlate = b.licensePlate?.plate || '';
        return this.sortOrder === 'desc'
          ? this.sortLicensePlates(bPlate, aPlate)
          : this.sortLicensePlates(aPlate, bPlate);
      }
      return 0;
    });

    return filteredData;
  }

  toggleSort(sortKey: string): void {
    this.sortOrder = this.sortOrder === 'desc' ? 'asc' : 'desc';
    this.sortKey = sortKey;
    this.updateData();
  }

  sortLicensePlates(a: string, b: string): number {
    return a.localeCompare(b, 'sv', { sensitivity: 'base' });
  }

  onSearch(searchText: string): void {
    this.searchText = searchText;
    this.updateData();
  }

  onSearchTextChanged(searchText: string): void {
    this.searchText = searchText;
    this.currentPage = 1;
    this.updateData();
  }

  updateData(): void {
    const filteredData = this.filterData(this.dataSubject.value);
    this.filteredDataSubject.next(filteredData);
    this.totalItems = filteredData.length;
  }

  onPageChange(newPage: number): void {
    this.currentPage = newPage;
  }

  getTotalItems(): number {
    return this.dataSubject.value.length;
  }

  get startIndex(): number {
    return (this.currentPage - 1) * this.pageSize;
  }

  get endIndex(): number {
    return Math.min(this.startIndex + this.pageSize, this.getTotalItems());
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

  openDetailsModal(item: any) {
    const modalRef = this.ngbModal.open(CrossingInfoComponent, { size: 'lg' });
    modalRef.componentInstance.item = item;
  }

  openCrossingChartModal() {
    const modalRef = this.ngbModal.open(CrossingChartComponent, { size: 'lg' });
    modalRef.componentInstance.data = this.data$;
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
