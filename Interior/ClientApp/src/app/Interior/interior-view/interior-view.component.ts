import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';
import { InteriorDataService } from 'src/app/services/KendoCenter.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-interior-view',
  templateUrl: './interior-view.component.html',
  styleUrls: ['./interior-view.component.scss']
})
export class InteriorViewComponent implements OnInit {
  public view: Observable<GridDataResult>;
  public sort:Array<SortDescriptor>=[];
  public pageSize=5;
  public skip=0;
  constructor(private service:InteriorDataService,private router:Router) {}
  ngOnInit():void {
    this.view=this.service;
    this.loadData();

  }
  public dataStateChange({ skip, take, sort }: DataStateChangeEvent): void {
      this.skip=skip;
      this.pageSize=take;
      this.sort=sort;
      //console.log(sort)
      this.loadData();
  }
  private loadData():void{
    this.service.query({skip:this.skip,take:this.pageSize,sort:this.sort});
  }
   editButtonClick(id:any){
      this.router.navigate(['/interiorEdit',id]);
  }
  createButtonClick(){
    this.router.navigate(['/interiorEdit']);
  }

}
