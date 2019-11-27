import { CategoryService } from './../../services/DataCenter.service';
import { CategoryEditModel } from './../../models/Category';
import { Component, OnInit, Input, AfterContentChecked, Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'app-category-attachment',
  templateUrl: './category-attachment.component.html',
  styleUrls: ['./category-attachment.component.scss'],
  providers:[CategoryService]
})
export class CategoryAttachmentComponent implements AfterContentChecked {

  categoryModels = new Array<CategoryEditModel>();
  @Input() selectedItemsFromSubCategory = [];
  @Input() selectedItemsFromCategory = [];
  @Output() onChangeItems=new EventEmitter();
  dropdownListFromCategory = [];
  dropdownListFromSubCategory = [];
  dropdownCategorySettings = {};
  dropdownSubCategorySettings={};
  constructor(private categoryService:CategoryService){
    this.categoryService.getCategoryAll().subscribe(s=>this.categoryModels = s["data"]["data"]);
  }
  ngAfterContentChecked() {
    this.dropdownListFromCategory = this.categoryModels.filter(s=>s.parentId==null);
    this.dropdownListFromCategory.forEach(e=>e.text=e.contents[0].text);
   
    this.dropdownCategorySettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'text',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      enableCheckAll:false
    };
    this.dropdownSubCategorySettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'text',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      closeDropDownOnSelection:false,
      enableCheckAll:false
    };
    console.log(this.selectedItemsFromCategory)

  }
  onItemDropDownCategory(item: any) {
    this.dropdownListFromSubCategory= this.categoryModels.filter(d=> this.selectedItemsFromCategory.map(s=>s.id).includes(d.parentId));
    this.dropdownListFromSubCategory.forEach(e=>e.text=e.contents[0].text);
    this.onChangeItems.emit(this.dropdownListFromSubCategory)
  }


  

}
