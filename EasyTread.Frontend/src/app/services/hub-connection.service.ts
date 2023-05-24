import { Subject } from 'rxjs';

import { Injectable, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

import { environment } from '../enviroment/enviroment';

@Injectable({
  providedIn: 'root',
})
export class HubConnectionService implements OnDestroy {
  private notificationHubUrl = environment.notificationHubUrl;

  private hubConnection: HubConnection;
  private notificationSubject = new Subject<any>();
  public notification$ = this.notificationSubject.asObservable();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.notificationHubUrl)
      .build();

    this.hubConnection.on('ReceiveNotification', (message) => {
      console.log('Received notification in HubConnectionService:', message);
      this.notificationSubject.next(message);
    });

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((error) =>
        console.error('Error while starting the connection:', error)
      );
  }

  ngOnDestroy() {
    this.hubConnection.stop();
  }
}
