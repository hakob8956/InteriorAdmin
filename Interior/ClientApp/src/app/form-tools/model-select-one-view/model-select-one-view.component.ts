import { Component, OnInit, AfterContentChecked, Input, EventEmitter, Output } from "@angular/core";
import { CategoryEditModel } from 'src/app/models/Category';

@Component({
  selector: "app-model-select-one-view",
  templateUrl: "./model-select-one-view.component.html",
  styleUrls: ["./model-select-one-view.component.scss"]
})
export class ModelSelectOneViewComponent implements AfterContentChecked {
  @Input() model:any;
  @Input() selectedId: number;
  @Output() onChangeId = new EventEmitter<number>();
  dropdownList = [];
  selectedItems:any;
  dropdownSettings = {};
  ngAfterContentChecked(): void {
    console.log(this.selectedId)
    this.dropdownList = this.model;
    this.dropdownList.forEach(e=>e.text=e.contents[0].text);
    if (this.selectedId) this.selectedItems = this.model.find(e=>e.id==this.selectedId);
    this.dropdownSettings = {
      singleSelection: true,
      idField: "id",
      textField: "text",
      allowSearchFilter: true,
      closeDropDownOnSelection:true,
      itemsShowLimit:10
    };   
  }
  onItemSelect(item: any) {
    this.onChangeId.emit(item.id);
  }
}
