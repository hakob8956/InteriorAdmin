import { ShopDataService } from './../../services/KendoCenter.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shop-view',
  templateUrl: './shop-view.component.html',
  styleUrls: ['./shop-view.component.scss']
})
export class ShopViewComponent implements OnInit {

  
  public view: Observable<GridDataResult>;

  constructor(private service:ShopDataService,private router:Router) { }

  ngOnInit() {
    this.view=this.service;
    this.loadData();
  }
  private loadData():void{
    //this.service.queryAll().subscribe(response=>console.log(response));
    this.service.query(null);
    
  }
  editButtonClick(id:any){
    this.router.navigate(['/shopEdit',id]);
  }
  createButtonClick(){
    this.router.navigate(['/shopEdit']);
  }

}
