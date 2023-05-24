import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
})
export class SearchBarComponent {
  @Output() searchTextChanged = new EventEmitter<string>();

  searchText = '';

  onSearchTextChange(): void {
    this.searchTextChanged.emit(this.searchText);
  }

  clearSearchField(): void {
    this.searchText = '';
    this.onSearchTextChange();
  }
}
