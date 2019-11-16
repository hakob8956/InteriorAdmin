import { OptionContentModel } from './../../../models/OptionDescription';
import { Component, OnInit, Input, Output,EventEmitter} from '@angular/core';
import { LanguageModel } from 'src/app/models/Language';
import { LanguageService } from 'src/app/services/DataCenter.service';

@Component({
  selector: 'app-option-detail-view',
  templateUrl: './option-detail-view.component.html',
  styleUrls: ['./option-detail-view.component.scss'],
  providers:[LanguageService]
})
export class OptionDetailViewComponent implements OnInit {
  @Input() model:OptionContentModel[] = new Array<OptionContentModel>();
  languageId:number=-1;
  languageModel: LanguageModel[] = [];
  @Output() onChangeOptionContent = new EventEmitter<OptionContentModel[]>();
  constructor(private languageService:LanguageService) { }
  ngOnInit() {
    this.languageService.getAllLanguages().subscribe(response => {
      this.languageModel = response;
      this.languageId=this.languageModel[0].id;
    });
    if(!this.model)
        this.model = new Array<OptionContentModel>();
    this.model[0].languageId
  }


  onChangeOption(option:OptionContentModel){
    this.model[option.id]=option;
    this.onChangeOptionContent.emit(this.model);
  }
  onAddOption(option:OptionContentModel){
    if(!this.model)
        this.model = new Array<OptionContentModel>();
    option.id=this.getRandomInt(10000);
    option.isCreate=true;
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
  changeIdLanguage(id: number) {
    this.languageId = id;
  }
  getRandomInt(max:number) {
    return Math.floor(Math.random() * Math.floor(max));
  }
}
