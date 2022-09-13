import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.sass']
})
export class EventosComponent implements OnInit {
public eventos:any=[];
public imageWidth = 150;
public imageMargin = 10;

public showImage:boolean=true;
private _filtroLista:string='';
eventosFiltrados:any;

public get filtroLista(){
  return this._filtroLista;
}
public set filtroLista(value:string){
  this._filtroLista = value;
  this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
}

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }
  public getEventos():void{
    this.http.get('https://localhost:5001/api/eventos')
    .subscribe(
      response=>{
        this.eventos = response,
        this.eventosFiltrados = this.eventos
      },
      error => console.error(error)
      )

  }
  public getImagePath(imageUrl:string):string{
    let pathSplitted = imageUrl.split('.');
    let path = `${pathSplitted[0]}.jpeg`;
    return path;
  }
  public toggleImage(){
    this.showImage = !this.showImage;
  }
  filtrarEventos(filtrarPor:string):any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento:any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 || evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

}
