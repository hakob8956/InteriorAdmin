import { OptionContentModel } from './../../../models/OptionDescription';
import { Component, OnInit, Output,EventEmitter} from '@angular/core';

@Component({
  selector: 'app-option-detail-add',
  templateUrl: './option-detail-add.component.html',
  styleUrls: ['./option-detail-add.component.scss']
})
export class OptionDetailAddComponent  {
 @Output() onAddOption=new EventEmitter<OptionContentModel>();
 option:OptionContentModel = new OptionContentModel();

 AddOption(){
    this.onAddOption.emit(this.option);
    this.option=new OptionContentModel();
 }
}
