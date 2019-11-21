import { AlertService } from './../../services/alert.service';
import { ContentModel } from "../../models/ContentModel";
import {
  LanguageService,
  CategoryService
} from "./../../services/DataCenter.service";
import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { LanguageModel } from "src/app/models/Language";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { CategoryEditModel } from "src/app/models/Category";
import { FileModel } from "src/app/models/File";

@Component({
  selector: "app-category-edit",
  templateUrl: "./category-edit.component.html",
  styleUrls: ["./category-edit.component.scss"],
  providers: [LanguageService, CategoryService]
})
export class CategoryEditComponent implements OnInit {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private categoryServce: CategoryService,
    private alertService:AlertService
  ) {}
  @ViewChild("labelImport")
  labelImport: ElementRef;
  categoryId: number;
  languageModel: LanguageModel;
  categoryModel: CategoryEditModel = new CategoryEditModel();
  currentLanguageId: number;
  fileName:string;
  ngOnInit() {
    this.categoryId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.categoryId) && this.categoryId > 0) {
      this.categoryServce.getCategory(this.categoryId).subscribe(response => {
        this.categoryModel = response["data"];
        console.log(this.categoryModel);
        this.initForm();
      });
    } else {
      this.categoryId = 0;
    }
  }
  initForm() {
    if (
      this.categoryModel.currentFile != null &&
      this.categoryModel.currentFile.fileName != null
    )
      this.fileName = this.categoryModel.currentFile.fileName;
    else this.fileName= "Choose file";
  }
  changeContents(currentContents:ContentModel[]){
    this.categoryModel.contents=currentContents;
    console.log(this.categoryModel.contents)
  }
  onFileChange(currentFile:any){
    this.categoryModel.file=currentFile.file;
  }
  
  cancelButton() {
    this.router.navigate(["/categoryView"]);
  }
  submitForm() {
    if (this.categoryModel.currentFile != null) {
      this.categoryModel.currentFile.imageData = null;
      this.categoryModel.currentFile.imageMimeType = null;
    } else {
      this.categoryModel.currentFile = new FileModel();
    }
    this.alertService.clear();
    if (this.categoryId == 0) {
       this.categoryModel.id = 0;
      
      this.categoryServce
        .createCategory(this.categoryModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    } else {
    
      this.categoryServce
        .editCategory(this.categoryModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    }
  }
  private checkValidRequest(success: Boolean) {
    if (success){
      this.alertService.success("Success",true);
      this.router.navigate(["/categoryView"]);
    } 
    else this.alertService.error("Error");
  }
}
