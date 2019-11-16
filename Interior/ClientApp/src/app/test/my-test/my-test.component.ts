import { OptionContentModel } from './../../models/OptionDescription';
import { CategoryService, InteriorService, LanguageService } from './../../services/DataCenter.service';
import { Component, OnInit } from '@angular/core';
import { ContentModel } from "src/app/models/ContentModel";
import { InteriorRequestModel } from 'src/app/models/Interior';
import { LanguageModel } from 'src/app/models/Language';

@Component({
  selector: 'app-my-test',
  templateUrl: './my-test.component.html',
  styleUrls: ['./my-test.component.scss'],
  providers:[InteriorService]
})
export class MyTestComponent  implements OnInit{
  constructor(private interiorService:InteriorService){}
  model:OptionContentModel[]=Array<OptionContentModel>();
  ngOnInit(): void {
    this.interiorService.getInteriorbyId(3).subscribe(s=>{
      this.model=s["data"].optionContents;
      console.log(this.model)
    });
  }
  onChangeOptionContent(model:OptionContentModel[]){
    console.log(model);
  }


 

}
