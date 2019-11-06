import { BrandDataService } from './../../services/KendoCenter.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { Router } from '@angular/router';

@Component({
  selector: 'app-brand-view',
  templateUrl: './brand-view.component.html',
  styleUrls: ['./brand-view.component.scss']
})
export class BrandViewComponent implements OnInit {

  
  public view: Observable<GridDataResult>;

  constructor(private service:BrandDataService,private router:Router) { }

  ngOnInit() {
    this.view=this.service;
    this.loadData();
  }
  private loadData():void{
    //this.service.queryAll().subscribe(response=>console.log(response));
    this.service.query(null);
    
  }
  editButtonClick(id:any){
    this.router.navigate(['/brandEdit',id]);
  }
  createButtonClick(){
    this.router.navigate(['/brandEdit']);
  }

}
