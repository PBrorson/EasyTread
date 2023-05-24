import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
})
export class DatePickerComponent {
  @Input() selectedDateRange!: Date[];
  @Output() selectedDateRangeChange = new EventEmitter<Date[]>();

  onSelectedDateRangeChange(newDateRange: Date[]): void {
    this.selectedDateRangeChange.emit(newDateRange);
  }
}
