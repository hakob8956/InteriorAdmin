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
import { Content } from "src/app/models/Content";
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
    private brandService: BrandService
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
        this.initForm();
      });
    } else {
      this.brandId = 0;
    }
  }

  initForm(){
    if (
      this.brandModel.currentFile != null &&
      this.brandModel.currentFile.fileName != null
    )
      this.fileName= this.brandModel.currentFile.fileName;
    else this.fileName= "Choose file";
    console.log(this.fileName);
  }
  changeContents(currentContents:Content[]){
    this.brandModel.contents=currentContents; 
  }
  onFileChange(currentFile:File){
    this.brandModel.file=currentFile;
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
    //this.brandModel.currentFile.fileName = this.labelImport.nativeElement.innerText;

    // console.log(this.brandModel);
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
    if (success) this.router.navigate(["/brandView"]);
    else alert("Error");
  }
}
