import { RecommendationDataService } from './../../services/KendoCenter.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { BrandDataService } from 'src/app/services/KendoCenter.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-recommendation-view',
  templateUrl: './recommendation-view.component.html',
  styleUrls: ['./recommendation-view.component.scss']
})
export class RecommendationViewComponent implements OnInit {

  public view: Observable<GridDataResult>;

  constructor(private service:RecommendationDataService,private router:Router) { }

  ngOnInit() {
    this.view=this.service;
    this.loadData();
  }
  private loadData():void{
    this.service.query(null);
    
  }
  editButtonClick(id:any){
    this.router.navigate(['/recommendationEdit',id]);
  }
  createButtonClick(){
    this.router.navigate(['/recommendationEdit']);
  }

}
