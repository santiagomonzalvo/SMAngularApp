import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MessageService } from './message.service';
import * as $ from 'jquery'; 

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public message: string | null = null;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.messageService.getMessage().subscribe(message => {
      this.message = message;
    });
  }

  //public getServerMessages(): void {
  //  this.messageService.getServerMessages();
  //}
  public getServerMessages(): void {
    $.ajax({
      url: 'https://localhost:5001/api/empleado/mensaje',
      method: 'GET',
      success: (message: string) => {
        this.message = message;
      },
      error: (error: any) => {
        console.error('Error al obtener mensaje desde el servidor:', error);
      }
    });
  }
}
