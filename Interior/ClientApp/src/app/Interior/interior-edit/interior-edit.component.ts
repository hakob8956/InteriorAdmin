import { CategoryEditModel } from './../../models/Category';
import { BrandDataService } from './../../services/KendoCenter.service';
import { BrandEditModel } from './../../models/Brand';
import { ShopModel } from "./../../models/Shop";
import { ShopService, BrandService, CategoryService } from "./../../services/DataCenter.service";
import { OptionContentModel } from "./../../models/OptionDescription";
import { InteriorRequestModel } from "./../../models/Interior";
import { LanguageService } from "src/app/services/DataCenter.service";
import { Component, OnInit, ViewChild, ElementRef, ɵConsole } from "@angular/core";
import { Observable } from "rxjs";
import { Router, ActivatedRoute } from "@angular/router";
import { InteriorService } from "src/app/services/DataCenter.service";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { LanguageModel } from "src/app/models/Language";
import { ContentModel } from "src/app/models/ContentModel";
import { ContentType, FileType } from "src/app/models/Enums";
@Component({
  selector: "app-interior-edit",
  templateUrl: "./interior-edit.component.html",
  styleUrls: ["./interior-edit.component.scss"],
  providers: [InteriorService, LanguageService, ShopService,BrandService,CategoryService]
})
export class InteriorEditComponent implements OnInit {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private interiorService: InteriorService,
    private languageService: LanguageService,
    private shopService: ShopService,
    private brandService:BrandService,
    private categoryService:CategoryService
  ) {}
  @ViewChild("labelImport")
  labelImport: ElementRef;
  fileToUpload: File = null;
  interiorId: number;
  languageModel: LanguageModel=new LanguageModel();
  interiorGetModel: InteriorRequestModel = new InteriorRequestModel();
  shopsModel: ShopModel[]=new Array<ShopModel>();
  brandsModel:BrandEditModel[] = new Array<BrandEditModel>();
  contentsModel: ContentModel[] = new Array<ContentModel>();
  categoriesModel:CategoryEditModel[]=new Array<CategoryEditModel>();
  currentLanguageId: number;
  get contentType() {
    return ContentType;
  }
  get fileType() {
    return FileType;
  }
  ngOnInit(): void {
    this.languageService.getAllLanguages().subscribe(response => {
      this.languageModel = response;
      this.currentLanguageId = this.languageModel[0].id;
    });
    this.brandService.getBrandAll().subscribe(r=>this.brandsModel=r["data"]["data"]);
    this.shopService.getShopAll().subscribe(r=>this.shopsModel=r["data"]["data"]);
    this.categoryService.getCateogryAll().subscribe(r=>this.categoriesModel=r["data"]["data"]);
    this.interiorId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.interiorId) && this.interiorId > 0) {
      this.interiorService
        .getInteriorbyId(this.interiorId)
        .subscribe(response => {
          this.interiorGetModel = response["data"];
          console.log(response["data"]);
        });
    } else {
      this.interiorId = 0;
    }
  }
  changeContents(model: ContentModel[]) {
    switch (model[0].type) {
      case ContentType.Name:
        this.interiorGetModel.nameContent = model;
        break;
      case ContentType.Description:
        this.interiorGetModel.descriptionContent = model;
        break;
    }
  }
  onFileChange(model: any) {
    switch (model.fileType) {
      case FileType.Image:
        this.interiorGetModel.imageFile = model.file;
        console.log(model);

        break;
      case FileType.AndroidBundle:
        this.interiorGetModel.androidFile = model.file;
        break;
      case FileType.IosBundle:
        this.interiorGetModel.iosFile = model.file;
        break;
      case FileType.Glb:
        this.interiorGetModel.glbFile = model.file;
        break;
    }
  }
  getFileName(fileType: FileType): string {
    try {
      switch (fileType) {
        case FileType.Image:
          return this.interiorGetModel.currentImageFile.fileName;
        case FileType.AndroidBundle:
          return this.interiorGetModel.currentAndroidFile.fileName;
        case FileType.IosBundle:
          return this.interiorGetModel.currentIosFile.fileName;
        case FileType.Glb:
          return this.interiorGetModel.currentGlbFile.fileName;
        default:
          return "Choose File";
      }
    } catch {
      return "Choose File";
    }
  }
  onChangeOptionContent(model: OptionContentModel[]) {
    this.interiorGetModel.optionContents = model;
    this.interiorGetModel.optionContents.forEach(e=>e.isCreate?e.id=0:e.id=e.id);
  }
  submitForm() {
    this.interiorGetModel.id=0;
    console.log(this.interiorGetModel);
    this.interiorService.createInterior(this.interiorGetModel).subscribe(r=>console.log(r));
  }
}