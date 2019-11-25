import { CategoryEditModel } from 'src/app/models/Category';
import { CategoryService } from './../../services/DataCenter.service';
import { Component, OnInit, ViewChild } from "@angular/core";
import { NgbTypeahead } from "@ng-bootstrap/ng-bootstrap";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/debounceTime";
import "rxjs/add/operator/distinctUntilChanged";
import "rxjs/add/operator/merge";
import "rxjs/add/operator/filter";



@Component({
  selector: "app-my-test",
  templateUrl: "./my-test.component.html",
  styleUrls: ["./my-test.component.scss"],
  providers: [CategoryService]
})
export class MyTestComponent implements OnInit {
  public categoryModels=new Array<CategoryEditModel>();
  constructor(private categoryService:CategoryService) {}
  ngOnInit(): void {
    this.categoryService.getCategoryAll().subscribe(s=>this.categoryModels=s["data"]["data"]);
  }
  
 
  
}
