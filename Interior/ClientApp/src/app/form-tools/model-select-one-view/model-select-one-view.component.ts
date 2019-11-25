import { Component, OnInit, AfterContentChecked, Input } from "@angular/core";

@Component({
  selector: "app-model-select-one-view",
  templateUrl: "./model-select-one-view.component.html",
  styleUrls: ["./model-select-one-view.component.scss"]
})
export class ModelSelectOneViewComponent implements AfterContentChecked {
  @Input() model: any;
  @Input() selectedId: number;
 // @Output() changeId:number = new EventEmitter<num
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  ngAfterContentChecked(): void {
    this.dropdownList = this.model;
    this.dropdownList.forEach(e=>e.text=e.contents[0].text);
    if (this.selectedId) this.selectedItems = this.model[this.selectedId];
    this.dropdownSettings = {
      singleSelection: true,
      idField: "id",
      textField: "text",
      allowSearchFilter: true,
      closeDropDownOnSelection:true,
      itemsShowLimit:10
    };
    console.log(this.dropdownList)
    
  }

  onItemSelect(item: any) {
    console.log(this.selectedItems);
  }
}
