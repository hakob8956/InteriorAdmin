import { AlertService } from './../../services/alert.service';
import { Injectable } from "@angular/core";
import {
  LanguageService,
  BrandService
} from "./../../services/DataCenter.service";
import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Form, FormGroup, FormControl, Validators } from "@angular/forms";
import { BrandEditModel } from "src/app/models/Brand";
import { LanguageModel } from "src/app/models/Language";
import { ContentModel } from "src/app/models/ContentModel";
import { FileModel } from "src/app/models/File";

@Component({
  selector: "app-brand-edit",
  templateUrl: "./brand-edit.component.html",
  styleUrls: ["./brand-edit.component.scss"],
  providers: [BrandService, LanguageService]
})
export class BrandEditComponent implements OnInit {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private brandService: BrandService,
    private alertService:AlertService
  ) {}
  @ViewChild("labelImport")
  labelImport: ElementRef;
  brandId: number;
  brandModel: BrandEditModel = new BrandEditModel();
  currentLanguageId: number;
  fileName:string="Choose File";
  ngOnInit() {
    this.brandId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.brandId) && this.brandId > 0) {
      this.brandService.getBrandbyId(this.brandId).subscribe(response => {
        this.brandModel = response["data"];
      });
    } else {
      this.brandId = 0;
    }
  }
  get getFile(){
    if(this.brandModel && this.brandModel.currentFile)
        return this.brandModel.currentFile;
    return null;
  }
  changeContents(currentContents:ContentModel[]){
    this.brandModel.contents=currentContents; 
  }
  onFileChange(currentFile:any){
    this.brandModel.file=currentFile.file;
  }
  cancelButton() {
    this.router.navigate(["/brandView"]);
  }
  submitForm() {
    if (this.brandModel.currentFile != null) {
      this.brandModel.currentFile.imageData = null;
      this.brandModel.currentFile.imageMimeType = null;
    } else {
      this.brandModel.currentFile = new FileModel();
    }
    this.alertService.clear();
    if (this.brandId == 0) {
      this.brandModel.id = 0;
      this.brandService
        .createBrand(this.brandModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    } else {
      this.brandService
        .editBrand(this.brandModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    }
  }
  private checkValidRequest(success: Boolean) {
    if (success){
      this.alertService.success("Success",true);
      this.router.navigate(["/brandView"]);
    } 
    else this.alertService.error("Error");
  }
}
