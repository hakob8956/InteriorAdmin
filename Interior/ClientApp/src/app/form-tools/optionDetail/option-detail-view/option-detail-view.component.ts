import { OptionContentModel } from './../../../models/OptionDescription';
import { Component, OnInit, Input, Output,EventEmitter} from '@angular/core';
import { LanguageService } from 'src/app/services/DataCenter.service';

@Component({
  selector: 'app-option-detail-view',
  templateUrl: './option-detail-view.component.html',
  styleUrls: ['./option-detail-view.component.scss'],
  providers:[LanguageService]
})
export class OptionDetailViewComponent implements OnInit {
  @Input() model:OptionContentModel[];
  @Input() languageId:number;
  @Output() onChangeOptionContent = new EventEmitter<OptionContentModel[]>();
  constructor(private languageService:LanguageService) { }
  ngOnInit() {
  }
  onChangeOption(option:OptionContentModel){
    this.model[option.id]=option;
    this.onChangeOptionContent.emit(this.model);
  }
  onAddOption(option:OptionContentModel){
    option.id=0;
    option.languageId=this.languageId;
    this.model.push(option);
    this.onChangeOptionContent.emit(this.model);

  }
  onDeleteOption(option:OptionContentModel){
     this.model = this.model.filter(function( obj ) {
      return obj.id !== option.id;
      });
     this.onChangeOptionContent.emit(this.model);
  }

}
