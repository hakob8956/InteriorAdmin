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
  providers:[]
})
export class MyTestComponent {
  currentShopId:number=0;
  currentInteriorId:number=0;
  currentCategoryId:number=0;
  currentBrandId:number=0;
  onChangeSelectionId(a:any){
    console.log(a);
  }

 

}
