import { OptionContentModel } from './../../models/OptionDescription';
import { CategoryService, InteriorService } from './../../services/DataCenter.service';
import { Component, OnInit } from '@angular/core';
import { Content } from 'src/app/models/Content';
import { InteriorRequestModel } from 'src/app/models/Interior';

@Component({
  selector: 'app-my-test',
  templateUrl: './my-test.component.html',
  styleUrls: ['./my-test.component.scss'],
  providers:[InteriorService]
})
export class MyTestComponent  implements OnInit{
  constructor(private interiorService:InteriorService){}
  model:OptionContentModel[]=[];
  ngOnInit(): void {
    this.interiorService.getInteriorbyId(2).subscribe(s=>{
      this.model=s["data"].optionContents;
      console.log(this.model)
    });
  }
  onChangeOptionContent(model:OptionContentModel[]){
     this.model=model;
     console.log(this.model)
  }
  

 

}
