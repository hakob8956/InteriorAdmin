import { CategoryService } from './../../services/DataCenter.service';
import { CategoryEditModel } from './../../models/Category';
import { Component, OnInit, Input, AfterContentChecked } from '@angular/core';

@Component({
  selector: 'app-category-attachment',
  templateUrl: './category-attachment.component.html',
  styleUrls: ['./category-attachment.component.scss'],
  providers:[CategoryService]
})
export class CategoryAttachmentComponent implements AfterContentChecked {

  categoryModels = new Array<CategoryEditModel>();
  dropdownListFromCategory = [];
  selectedItemsFromCategory = [];
  dropdownListFromSubCategory = [];
  selectedItemsFromSubCategory = [];
  dropdownSettings = {};
  constructor(private categoryService:CategoryService){
    this.categoryService.getCategoryAll().subscribe(s=>this.categoryModels = s["data"]["data"]);
  }
  ngAfterContentChecked() {
    this.dropdownListFromCategory = this.categoryModels.filter(s=>s.parentId==null);
    this.dropdownListFromCategory.forEach(e=>e.text=e.contents[0].text);
   
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'text',
      itemsShowLimit: 5,
      allowSearchFilter: true
    };

  }
  onItemSelectCategory(item: any) {
    console.log(this.selectedItemsFromCategory.map(s=>s.id))
    this.dropdownListFromSubCategory= this.categoryModels.filter(s=> this.selectedItemsFromCategory.map(s=>s.id).includes(s.id));
    this.dropdownListFromSubCategory.forEach(e=>e.text=e.contents[0].text);
  }

  

}
