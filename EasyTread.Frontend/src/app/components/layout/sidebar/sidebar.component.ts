import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
})
export class SidebarComponent {
  constructor(private router: Router) {}

  goToCrossingDetails() {
    this.router.navigate(['/crossing-details']);
  }

  goToCrossingBadTires() {
    this.router.navigate(['/crossing-bad-tires']);
  }
}
