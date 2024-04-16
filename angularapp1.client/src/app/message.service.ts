import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private messageSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  constructor(private http: HttpClient) { }

  public getMessage(): Observable<string | null> {
    return this.messageSubject.asObservable();
  }

  public getServerMessages(): void {
    this.http.get<string>('https://localhost:5001/api/empleado/message').subscribe(
      message => {
        this.messageSubject.next(message);
      },
      error => {
        console.error('Error al obtener mensaje desde el servidor:', error);
      }
    );
  }
}
