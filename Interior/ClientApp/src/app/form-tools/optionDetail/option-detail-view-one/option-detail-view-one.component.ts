import { OptionContentModel } from './../../../models/OptionDescription';
import { Component, OnInit, Input,EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-option-detail-view-one',
  templateUrl: './option-detail-view-one.component.html',
  styleUrls: ['./option-detail-view-one.component.scss']
})
export class OptionDetailViewOneComponent implements OnInit {
  @Input() option:OptionContentModel=new OptionContentModel();
  @Output() onChangeOption = new EventEmitter<OptionContentModel>();
  @Output() onDeleteOption = new EventEmitter<OptionContentModel>();

  constructor() { }

  ngOnInit() {
  }
  onClick(){
    console.log(this.option)
  }

}
