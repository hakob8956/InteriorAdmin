import { SectionType } from './../../../models/Enums';
import { InteriorRequestModel } from './../../../models/Interior';
import { ShopModel } from 'src/app/models/Shop';
import { CategoryEditModel } from 'src/app/models/Category';
import {
  CategoryService,
  BrandService,
  ShopService,
  InteriorService
} from "./../../../services/DataCenter.service";
import { Component, OnInit, Output,EventEmitter, Input} from "@angular/core";
import { BrandEditModel } from 'src/app/models/Brand';

@Component({
  selector: "app-choose-recommend-view",
  templateUrl: "./choose-recommend-view.component.html",
  styleUrls: ["./choose-recommend-view.component.scss"],
  providers: [ShopService, InteriorService, CategoryService, BrandService]
})
export class ChooseRecommendViewComponent implements OnInit {
  constructor(
    private categoryService: CategoryService,
    private brandService: BrandService,
    private shopService: ShopService,
    private interiorService: InteriorService
  ) {}
//  @Output() onChangeShopId = new EventEmitter<number>();
   currentShopId:number=0;
   currentInteriorId:number=0;
   currentCategoryId:number=0;
   currentBrandId:number=0;
  
  
  categoryModels:CategoryEditModel[]= new Array<CategoryEditModel>();
  brandModels:BrandEditModel[]= new Array<BrandEditModel>();
  shopModels:ShopModel[]= new Array<ShopModel>();
  interiorModels:InteriorRequestModel[]= new Array<InteriorRequestModel>();
  currentSectionType:SectionType=SectionType.Shop;
  sectionType(){return SectionType; }

  ngOnInit() {
    this.categoryService.getCategoryAll().subscribe(r=>this.categoryModels=r["data"]["data"]);
    this.brandService.getBrandAll().subscribe(r=>this.brandModels=r["data"]["data"]);
    this.shopService.getShopAll().subscribe(r=>this.shopModels=r["data"]["data"]);
    this.interiorService.getAllInterior().subscribe(r=>this.categoryModels=r["data"]["data"]);
  }
  onClick(){
    console.log(this.currentSectionType)

  }
  changeSectionType(section){
    console.log(section)
    this.sectionType =section; 
  }
}
