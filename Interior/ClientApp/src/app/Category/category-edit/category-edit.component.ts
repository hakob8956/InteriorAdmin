import { LanguageService, CategoryService } from './../../services/DataCenter.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { LanguageShowModel } from 'src/app/models/Language';
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss'],
  providers:[LanguageService,CategoryService]
})
export class CategoryEditComponent implements OnInit {

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private languageService: LanguageService,
    private categoryServce:CategoryService
  ) {}
  @ViewChild("labelImport")
  labelImport: ElementRef;
  faSearch = faSearch;
  form: FormGroup;
  fileToUpload: File = null;
  categoryId: number;
  languageModel:any;
  categoryModel:any;
  currentLanguageId:number;
  ngOnInit() {
    this.form = new FormGroup({
      language: new FormControl("Loading...", Validators.required),
      importFile: new FormControl("")
    });
    this.languageService.getAllLanguages().subscribe(response => {
      this.languageModel = response;
    });
    this.categoryId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.categoryId) && this.categoryId > 0) {
      this.categoryServce.getCategory(this.categoryId).subscribe(response => {
        this.categoryModel = response["data"];
        this.currentLanguageId=+this.categoryModel.contents[0].languageId;
        console.log(this.categoryModel)
        this.initForm();
      });
      
    } else {
      this.categoryId = 0;
    }
    
  }
  initForm() {
    console.log(this.languageModel[0].name);
    this.form
      .get("language")
      .setValue(
        this.languageModel[0].name != null
          ? this.languageModel[0].name
          : ""
      );
    this.labelImport.nativeElement.innerText =
      this.categoryModel.fileName != null
        ? this.categoryModel.fileName
        : "Choose file";
   
  }
  onFileChange(files: FileList) {
    this.labelImport.nativeElement.innerText = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
  }
  onChange(value){
    this.currentLanguageId=+value;
    console.log(this.currentLanguageId)
  }
  cancelButton(){
    console.log(this.form.get("language").value)
  }
  submitForm(){

  }


}
