import { Component, Input } from '@angular/core';
import { Subject } from 'rxjs';
import { CrossingResponse } from 'src/app/domain/client';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
})
export class NotificationComponent {
  @Input() count = 0;
  @Input() newAlerts = 0;
  @Input() latestObjects: CrossingResponse[] = [];

  showNotification = false;
  latestData: any[] = [];
  latestDataSubject = new Subject<any[]>();
  dismissed = false;

  constructor() {
    this.latestDataSubject.subscribe((latestData) => {
      this.latestData = latestData;
    });
  }

  get hasAlerts(): boolean {
    return this.count > 0;
  }

  toggleNotification(): void {
    this.showNotification = !this.showNotification;
    if (this.showNotification) {
      this.newAlerts = 0;
    }
  }

  dismissNotification(): void {
    console.log('Before reset: newAlerts =', this.newAlerts);
    this.newAlerts = 0;

    console.log('After reset: newAlerts =', this.newAlerts);
    this.showNotification = false;
    this.newAlerts = 0;
  }
}
