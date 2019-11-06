import {  LanguageService,ShopService} from './../../services/DataCenter.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { Form, FormGroup, FormControl, Validators } from '@angular/forms';
import { ShopEditModel } from 'src/app/models/Shop';
import { LanguageGetModel } from 'src/app/models/Language';
import { Content } from 'src/app/models/Content';


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
    private languageService: LanguageService,
    private shopService:ShopService
  ) {}

  @ViewChild("labelImport")
  labelImport: ElementRef;
  faSearch = faSearch;
  fileToUpload: File = null;
  shopId: number;
  form:FormGroup;
  languageModel: LanguageGetModel;
  shopModel: ShopEditModel = new ShopEditModel();
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
    console.log(this.languageModel[0].name);
    // this.form
    //   .get("language")
    //   .setValue(
    //     this.languageModel[0].name != null ? this.languageModel[0].name : ""
    //   );
    this.labelImport.nativeElement.innerText =
      this.shopModel.fileName != null
        ? this.shopModel.fileName
        : "Choose file";
  }
  inputTextChange(element: any) {
    if(this.shopModel.contents != null)
        this.contentsModel = this.shopModel.contents;
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
    this.shopModel.contents=this.contentsModel;
    console.log(this.shopModel);
  }
  getCurrentIdFromContentModel(languageId:number):number{
    let result:number = 0;
    if (this.shopId > 0)
    {
      this.shopModel.contents.forEach(el=>{
        if(languageId == el.languageId){
            result=el.id;
        }
      });
    }
    return result;
    
  }
  getTextFromShop(languageId: number): string {
    let output: string = "";
    if (this.shopId == 0) {
      return output;
    }
    try {
      this.shopModel.contents.forEach(element => {
        if (element != null && languageId == element.languageId) {
          output = element.text;
        }
      });
    } catch {
      return output;
    }

    return output;
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
    this.router.navigate(["/shopView"]);
  }
  submitForm() {
    this.shopModel.file = this.fileToUpload;
    this.shopModel.fileName = this.labelImport.nativeElement.innerText;
    // console.log(this.shopModel);
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
    if (success) this.router.navigate(["/shopView"]);
    else alert("Error");
  }
}

