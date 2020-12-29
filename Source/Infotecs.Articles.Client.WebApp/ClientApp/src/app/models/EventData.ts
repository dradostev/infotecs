export class EventData {
  constructor (name: string, payload: any) {
    this.name = name;
    this.payload = payload;
  }

  public name: string;

  public payload: any;
}
