import { ViewEncapsulation } from '@angular/core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoriesService } from '../northwind.service';
import { State,SortDescriptor } from '@progress/kendo-data-query';
import {
    GridDataResult,
    DataStateChangeEvent
} from '@progress/kendo-angular-grid';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
 // styleUrls: ['../app.component.scss','./dashboard.component.scss'],
  //encapsulation: ViewEncapsulation.None
})
export class DashboardComponent  {

 
}
