import {  LanguageService } from './../../services/DataCenter.service';
import { Component, OnInit } from '@angular/core';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State, SortDescriptor } from '@progress/kendo-data-query';
import { Observable } from 'rxjs';
import { LanguageDataService } from '../../services/KendoCenter.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-language-view',
  templateUrl: './language-view.component.html',
  styleUrls: ['./language-view.component.scss'],
  providers:[LanguageService]
})
export class LanguageViewComponent implements OnInit {

  public view: Observable<GridDataResult>;

  constructor(private service:LanguageDataService,private router:Router) { }

  ngOnInit() {
    this.view=this.service;
    this.loadData();
  }
  private loadData():void{
    //this.service.queryAll().subscribe(response=>console.log(response));
    this.service.query(null);
    
  }
  editButtonClick(id:any){
    this.router.navigate(['/languageEdit',id]);
  }
  createButtonClick(){
    this.router.navigate(['/languageEdit']);
  }

}
