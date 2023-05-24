import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  HostBinding,
  Input,
  OnChanges,
  Output,
} from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css'],
})
export class PaginationComponent implements OnChanges {
  @Input() currentPage = 1;
  @Input() totalItems = 0;
  @Input() pageSize = 10;
  @Output() pageChange = new EventEmitter<number>();

  @HostBinding('class.pagination') paginationClass = true;

  constructor(private changeDetector: ChangeDetectorRef) {}

  currentPageRange: number[] = [];

  changePage(newPage: number): void {
    this.pageChange.emit(newPage);
  }

  generatePagesArray(): number[] {
    const totalPages = this.getTotalPages();
    const pagesToShow = 5;
    let startPage = 1;
    let endPage = totalPages;

    if (totalPages > pagesToShow) {
      const middlePage = Math.ceil(pagesToShow / 2);
      if (this.currentPage <= middlePage) {
        startPage = 1;
        endPage = pagesToShow;
      } else if (this.currentPage + middlePage >= totalPages) {
        startPage = totalPages - pagesToShow + 1;
        endPage = totalPages;
      } else {
        startPage = this.currentPage - middlePage + 1;
        endPage = this.currentPage + middlePage - 1;
      }
    }

    return Array.from(
      { length: endPage - startPage + 1 },
      (_, i) => startPage + i
    );
  }

  getTotalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  goToFirstPage(): void {
    this.changePage(1);
  }

  goToLastPage(): void {
    this.changePage(this.getTotalPages());
  }

  ngOnChanges(): void {
    this.changeDetector.detectChanges();
  }
}
