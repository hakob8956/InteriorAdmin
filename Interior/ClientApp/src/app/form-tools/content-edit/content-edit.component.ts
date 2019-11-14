import { LanguageService } from 'src/app/services/DataCenter.service';
import { Component, OnInit, Input,Output,EventEmitter } from '@angular/core';
import { Content } from 'src/app/models/Content';
import { LanguageModel } from 'src/app/models/Language';

@Component({
  selector: 'app-content-edit',
  templateUrl: './content-edit.component.html',
  styleUrls: ['./content-edit.component.scss'],
  providers:[LanguageService]
})
export class ContentEditComponent implements OnInit{
   constructor(private languageService:LanguageService) { }
   @Input() currentContent:Content[]=[];
   @Input() contentType:ContentType;
   @Input() isCreate:boolean;
   @Input() labelName:string;
   @Output() changeContents =new EventEmitter<Content[]>();
  currentLanguageId:number;
  languageModel:LanguageModel;
  ngOnInit() {
    this.languageService.getAllLanguages().subscribe(response => {
      this.languageModel = response;
      this.currentLanguageId = this.languageModel[0].id;
    });
    if(this.currentContent == null)
        this.currentContent=[];
  }

  getText(languageId: number): string {
    let output: string = "";
    if (this.currentContent == null) {
        return output;
    }
    try {
        this.currentContent.forEach(element => {
            if (element != null && languageId == element.languageId) {
                output = element.text;
            }
        });
    } catch {
        return output;
    }

    return output;
  }
  onChange(value) {
    this.currentLanguageId = +value;
  }
  inputTextChange(element: any) {

    let ispush: boolean = true;
    this.currentContent.forEach(el => {
      if (el.languageId == +element.name) {
        el.text = element.value;
        ispush = false;
      }
    });
    if (ispush) {
      this.currentContent.push({
        id: this.getCurrentIdFromContentModel(+element.name),
        languageId: +element.name,
        text: element.value,
        type:this.contentType
      });
    }
    this.changeContents.emit(this.currentContent);
  }
  getCurrentIdFromContentModel(languageId: number): number {
    let result: number = 0;
    if (!this.isCreate) {
      this.currentContent.forEach(el => {
        if (languageId == el.languageId) {
          result = el.id;
        }
      });
    }
    return result;
  }
  
}
