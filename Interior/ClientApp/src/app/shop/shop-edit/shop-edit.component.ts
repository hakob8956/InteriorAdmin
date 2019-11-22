import { AlertService } from './../../services/alert.service';
import {  LanguageService,ShopService} from './../../services/DataCenter.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Form, FormGroup, FormControl, Validators } from '@angular/forms';
import { ShopModel } from 'src/app/models/Shop';
import { LanguageModel } from 'src/app/models/Language';
import { ContentModel } from "src/app/models/ContentModel";
import { FileModel } from 'src/app/models/File';


@Component({
  selector: 'app-shop-edit',
  templateUrl: './shop-edit.component.html',
  styleUrls: ['./shop-edit.component.scss'],
  providers:[ShopService,LanguageService]
})
export class ShopEditComponent implements OnInit {

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private shopService:ShopService,
    private alertService:AlertService
  ) {}

  shopId: number;
  languageModel: LanguageModel;
  shopModel: ShopModel = new ShopModel();
  currentLanguageId: number;
  fileName:string;
  ngOnInit() {


   this.shopId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.shopId) && this.shopId > 0) {
      this.shopService.getShopbyId(this.shopId).subscribe(response => {
        this.shopModel = response["data"];
        console.log(this.shopModel);
        this.initForm();
      });
    } else {
      this.shopId = 0;
    }
  }
  initForm() {
    if (
      this.shopModel.currentFile != null &&
      this.shopModel.currentFile.fileName != null
    )
      this.fileName = this.shopModel.currentFile.fileName;
    else this.fileName = "Choose file";
  }
  get getFile(){
    if(this.shopModel && this.shopModel.currentFile)
        return this.shopModel.currentFile;
    return null;
  }
  onFileChange(currentFile:any){
    this.shopModel.file=currentFile.file;
  }
  changeContents(currentContents:ContentModel[]){
    this.shopModel.contents=currentContents;
  } 
  cancelButton() {
    this.router.navigate(["/shopView"]);
  }
  submitForm() {
    if (this.shopModel.currentFile != null) {
      this.shopModel.currentFile.imageData = null;
      this.shopModel.currentFile.imageMimeType = null;
    } else {
      this.shopModel.currentFile = new FileModel();
    }
    this.alertService.clear();
    if (this.shopId == 0) {
      this.shopModel.id = 0;
      this.shopService
        .createShop(this.shopModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    } else {
      this.shopService
        .editShop(this.shopModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    }
  }
  private checkValidRequest(success: Boolean) {
    if (success){
      this.alertService.success("Success",true);
      this.router.navigate(["/shopView"]);
    } 
    else this.alertService.error("Error");
  }
}

