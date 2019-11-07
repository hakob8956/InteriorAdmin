import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';
import { InteriorDataService } from 'src/app/services/KendoCenter.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-interior-edit',
  templateUrl: './interior-edit.component.html',
  styleUrls: ['./interior-edit.component.scss']
})
export class InteriorEditComponent implements OnInit {
  ngOnInit(): void {
    ;
  }


}
