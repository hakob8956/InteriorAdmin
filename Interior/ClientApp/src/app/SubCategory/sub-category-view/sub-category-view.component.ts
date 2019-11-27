import { Component, OnInit } from '@angular/core';
import { CategoryDataService, LanguageDataService } from '../../services/KendoCenter.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';

@Component({
  selector: 'app-sub-category-view',
  templateUrl: './sub-category-view.component.html',
  styleUrls: ['./sub-category-view.component.scss']
})
export class SubCategoryViewComponent implements OnInit {

  public view: Observable<GridDataResult>;

  constructor(private service:CategoryDataService,private router:Router) { }

  ngOnInit() {
    this.view=this.service;
    this.loadData();
  }
  private loadData():void{
    //this.service.queryAll().subscribe(response=>console.log(response));
    this.service.query({onlySubCategories:true});
    
  }
  editButtonClick(id:any){
    this.router.navigate(['/categoryEdit',id]);
  }
  createButtonClick(){
    this.router.navigate(['/categoryEdit']);
  }

}
