import { CategoryEditModel } from './../../models/Category';
import { Component, OnInit, Input, AfterContentChecked } from '@angular/core';

@Component({
  selector: 'app-category-attachment',
  templateUrl: './category-attachment.component.html',
  styleUrls: ['./category-attachment.component.scss'],
})
export class CategoryAttachmentComponent implements AfterContentChecked {

  @Input() categoryModels = new Array<CategoryEditModel>();
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  ngAfterContentChecked() {
  
    console.log(this.categoryModels)
    this.dropdownList = [
      { item_id: 1, item_text: 'Mumbai' },
      { item_id: 2, item_text: 'Bangaluru' },
      { item_id: 3, item_text: 'Pune' },
      { item_id: 4, item_text: 'Navsari' },
      { item_id: 5, item_text: 'New Delhi' }
    ];
    this.selectedItems = [
      { item_id: 3, item_text: 'Pune' },
      { item_id: 4, item_text: 'Navsari' }
    ];
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      itemsShowLimit: 5,
      allowSearchFilter: true
    };
  }
  onItemSelect(item: any) {
    console.log(this.selectedItems);
  }

  

}
