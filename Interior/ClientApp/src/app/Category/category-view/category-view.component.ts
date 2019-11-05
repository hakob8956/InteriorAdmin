import { Component, OnInit } from '@angular/core';
import { CategoryDataService, LanguageDataService } from '../../services/KendoCenter.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';
@Component({
  selector: 'app-category-view',
  templateUrl: './category-view.component.html',
  styleUrls: ['./category-view.component.scss']
})
export class CategoryViewComponent implements OnInit {

  public view: Observable<GridDataResult>;

  constructor(private service:CategoryDataService,private router:Router) { }

  ngOnInit() {
    this.view=this.service;
    this.loadData();
  }
  private loadData():void{
    //this.service.queryAll().subscribe(response=>console.log(response));
    this.service.query(null);
    
  }
  editButtonClick(id:any){
    this.router.navigate(['/categoryEdit',id]);
  }
  createButtonClick(){
    this.router.navigate(['/categoryEdit']);
  }


}
