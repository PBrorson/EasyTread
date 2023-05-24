import { map, Observable } from 'rxjs';
import { CrossingClient } from 'src/app/domain/client';

import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { CrossingInfoComponent } from '../crossing-info/crossing-info.component';

@Component({
  selector: 'app-crossing-details',
  templateUrl: './crossing-details.component.html',
  styleUrls: ['./crossing-details.component.css'],
})
export class CrossingDetailsComponent {
  page: number = 1;
  itemsPerPage: number = 10;
  totalPages!: number;

  searchText: string = '';

  myData$: Observable<any>;

  form: FormGroup;

  constructor(
    private crossingClient: CrossingClient,
    private router: Router,
    private ngbModal: NgbModal, // Ska vi kanske köra med NgbActiveModal?

    private formBuilder: FormBuilder
  ) {
    this.myData$ = this.crossingClient.getAllCrossingVehicles();

    this.form = this.formBuilder.group({
      searchText: [''],
    });

    this.myData$
      .pipe(
        map((data) => {
          this.totalPages = Math.ceil(data.length / this.itemsPerPage);
        })
      )
      .subscribe();
  }

  ngOnInit() {
    this.crossingClient.getAllCrossingVehicles().subscribe((items) => {
      console.log('Received items: ', items);
    });
  }

  getPageRange(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  filterTable() {
    const searchText = this.form?.get('searchText')?.value ?? '';

    if (!searchText) {
      this.myData$ = this.crossingClient.getAllCrossingVehicles();
    } else {
      this.myData$ = this.crossingClient
        .getAllCrossingVehicles()
        .pipe(
          map((data) =>
            data.filter((item) =>
              item.licensePlate.plate
                .toLowerCase()
                .includes(searchText.toLowerCase())
            )
          )
        );
    }
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

  getStatusInfo(status: number): any {
    return this.statusInfo[status] || { icon: '', message: '' };
  }

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
