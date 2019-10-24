import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { CategoriesService } from '../northwind.service';
import { DataCenterService } from '../services/DataCenter.service';

@Component({
  selector: 'app-table-user-review',
  templateUrl: './table-user-review.component.html',
  styleUrls: ['./table-user-review.component.scss']
})
export class TableUserReviewComponent implements OnInit {


  public view: Observable<GridDataResult>;
  public state: State = {
      skip: 0,
      take: 5,
  };

  constructor(private service: CategoriesService,private dataCenter:DataCenterService) {}
  ngOnInit():void {
    this.view=this.dataCenter;

    this.loadData();

  }
  public dataStateChange({ skip, take, sort }: DataStateChangeEvent): void {
      this.state.skip=skip;
      this.state.take=take;
      this.state.sort=sort;
      
      this.loadData();
  }
  private loadData():void{
    //this.service.query({skip:this.state.skip,take:this.state.take,sort:this.state.sort});
    this.dataCenter.getUserReviewInfos({skip:this.state.skip,take:this.state.take});
  }

}
