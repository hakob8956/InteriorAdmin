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
  @Output() onChangeId = new EventEmitter();
  @Input() currentShopId:number=0;
  @Input() currentInteriorId:number=0;
  @Input() currentCategoryId:number=0;
  @Input() currentBrandId:number=0;
  
  
  categoryModels:CategoryEditModel[]= new Array<CategoryEditModel>();
  brandModels:BrandEditModel[]= new Array<BrandEditModel>();
  shopModels:ShopModel[]= new Array<ShopModel>();
  interiorModels:InteriorRequestModel[]= new Array<InteriorRequestModel>();
  mySectionType = SectionType;

  currentSectionType:SectionType=this.mySectionType.Shop;

  ngOnInit() {
    this.categoryService.getCategoryAll().subscribe(r=>this.categoryModels=r["data"]["data"]);
    this.brandService.getBrandAll().subscribe(r=>this.brandModels=r["data"]["data"]);
    this.shopService.getShopAll().subscribe(r=>this.shopModels=r["data"]["data"]);
    this.interiorService.getAllInterior().subscribe(r=>this.interiorModels=r["data"]["data"]);
  }
  onChange(value){
    console.log(value)
      this.onChangeId.emit({value:value,type:this.currentSectionType});
  }
  onClick(){
    console.log(this.currentInteriorId)
  }
}
