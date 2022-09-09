import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.sass']
})
export class EventosComponent implements OnInit {
public eventos:any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }
  public getEventos():void{
    this.http.get('https://localhost:5001/api/eventos')
    .subscribe(
      response=>this.eventos = response,
      error => console.error(error)
      )

  }

}
