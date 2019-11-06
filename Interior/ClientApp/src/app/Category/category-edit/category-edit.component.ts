import { Content } from './../../models/Content';
import {
  LanguageService,
  CategoryService
} from "./../../services/DataCenter.service";
import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { LanguageGetModel } from "src/app/models/Language";
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { CategoryEditModel } from "src/app/models/Category";

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
    private languageService: LanguageService,
    private categoryServce: CategoryService
  ) {}
  @ViewChild("labelImport")
  labelImport: ElementRef;
  faSearch = faSearch;
  form: FormGroup;
  fileToUpload: File = null;
  categoryId: number;
  languageModel: LanguageGetModel;
  categoryModel: CategoryEditModel = new CategoryEditModel();
  contentsModel: Content[] = [];
  currentLanguageId: number;
  ngOnInit() {
    this.form = new FormGroup({
      language: new FormControl("Loading...", Validators.required),
      importFile: new FormControl("")
    });

    this.languageService.getAllLanguages().subscribe(response => {
      this.languageModel = response;
      this.currentLanguageId = this.languageModel[0].id;
      console.log(this.currentLanguageId);
    });
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
    console.log(this.languageModel[0].name);
    // this.form
    //   .get("language")
    //   .setValue(
    //     this.languageModel[0].name != null ? this.languageModel[0].name : ""
    //   );
    this.labelImport.nativeElement.innerText =
      this.categoryModel.fileName != null
        ? this.categoryModel.fileName
        : "Choose file";
  }
  inputTextChange(element: any) {
    if(this.categoryModel.contents != null)
        this.contentsModel = this.categoryModel.contents;
    let ispush: boolean = true;
    this.contentsModel.forEach(el => {
      if (el.languageId == +element.name) {
        el.text = element.value;
        ispush = false;
      }
    });
    if (ispush) {
      this.contentsModel.push({
        id: this.getCurrentIdFromContentModel(+element.name),
        languageId: +element.name,
        text: element.value
      });
    }
    this.categoryModel.contents=this.contentsModel;
    console.log(this.categoryModel);
  }
  getCurrentIdFromContentModel(languageId:number):number{
    let result:number = 0;
    if (this.categoryId > 0)
    {
      this.categoryModel.contents.forEach(el=>{
        if(languageId == el.languageId){
            result=el.id;
        }
      });
    }
    return result;
    
  }
  onFileChange(files: FileList) {
    this.labelImport.nativeElement.innerText = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
  }
  onChange(value) {
    this.currentLanguageId = +value;
    console.log(this.currentLanguageId);
  }
  cancelButton() {
    this.router.navigate(["/categoryView"]);
  }
  submitForm() {
    this.categoryModel.file = this.fileToUpload;
    this.categoryModel.fileName = this.labelImport.nativeElement.innerText;
    // console.log(this.categoryModel);
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
    if (success) this.router.navigate(["/categoryView"]);
    else alert("Error");
  }
}
