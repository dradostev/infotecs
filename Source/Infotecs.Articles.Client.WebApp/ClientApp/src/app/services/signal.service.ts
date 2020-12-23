import {Inject, Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@aspnet/signalr";

@Injectable({
  providedIn: 'root'
})
export class SignalService {
  public connection: HubConnection;

  constructor(@Inject('BASE_URL') private baseUrl: string) {
    this.connection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + 'events')
      .build();

    this.startConnection();
  }

  private startConnection() {
    this.connection
      .start()
      .then(() => {
        console.info('Connection established.')
      })
      .catch(err => {
        console.error('Error establishing connection. Retrying...');
        setTimeout(() => this.startConnection(), 3000);
      })
  }
}
