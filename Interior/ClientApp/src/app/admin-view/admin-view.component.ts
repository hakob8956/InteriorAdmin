import { AdminsService } from './../services/DataCenter.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State, SortDescriptor } from '@progress/kendo-data-query';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
})
export class AdminViewComponent implements OnInit {

  public view: Observable<GridDataResult>;
  public sort:Array<SortDescriptor>=[];
  public pageSize=10;
  public skip=0;
  // public state: State = {
  //     skip: 0,
  //     take: 5  
  // };

  constructor(private service:AdminsService) {}
  ngOnInit():void {
    this.view=this.service;
    this.loadData();

  }
  public dataStateChange({ skip, take, sort }: DataStateChangeEvent): void {
      this.skip=skip;
      this.pageSize=take;
      this.sort=sort;
      
      this.loadData();
  }
  private loadData():void{
    //this.service.query({skip:this.state.skip,take:this.state.take,sort:this.state.sort});
   // console.log(this.state + '  ' + this.state.take);
    this.service.query({skip:this.skip,take:this.pageSize,sort:this.sort});
    //console.log(this.dataCenter);
  }

}
