import { MessageBox } from '../../models/MessageBox';
import { Router } from '@angular/router';
import { UserDataService } from '../../services/KendoCenter.service';
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
  public messageBox:MessageBox;
  // public state: State = {
  //     skip: 0,
  //     take: 5  
  // };

  constructor(private service:UserDataService,private router:Router) {}
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
      this.router.navigate(['/adminEdit',id]);
  }
  changePasswordButtonClick(id:any){
    this.router.navigate(['/adminChangePassword',id]);

  }
  createButtonClick(){
    this.router.navigate(['/adminEdit']);
  }
  onSearch(text:string){
      console.log(text)
  }

}
