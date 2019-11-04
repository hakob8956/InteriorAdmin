import { Router, ActivatedRoute } from "@angular/router";
import {
  Component,
  OnInit,
  ElementRef,
  EventEmitter,
  ViewChild
} from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import { LanguageEditModel, LanguageGetModel } from "src/app/models/Language";
import { LanguageService } from "src/app/services/DataCenter.service";
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: "app-language-edit",
  templateUrl: "./language-edit.component.html",
  styleUrls: ["./language-edit.component.scss"],
  providers: [LanguageService]
})
export class LanguageEditComponent implements OnInit {
  faSearch = faSearch;
  @ViewChild("labelImport")
  labelImport: ElementRef;
  languageEditModel: LanguageEditModel;
  languageGetModel: LanguageGetModel;
  form: FormGroup;
  fileToUpload: File = null;
  isLanguageCreate: boolean;
  languageId: number;
  imageSource:any;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private languageService: LanguageService
  ) {}
  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl("", Validators.required),
      codeName: new FormControl("", Validators.required),
      importFile: new FormControl("")
    });
    this.languageId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.languageId) && this.languageId > 0) {
      this.isLanguageCreate = false;
      this.languageService.getLanguage(this.languageId).subscribe(response => {
        this.languageGetModel = response["data"];
        this.initForm();
      });
      
    } else {
      this.languageId = 0;
      this.isLanguageCreate = true;
    }
  }
  initForm() {
    console.log(this.languageGetModel)
    this.form
      .get("name")
      .setValue(
        this.languageGetModel.name != null
          ? this.languageGetModel.name
          : ""
      );
    this.form
      .get("codeName")
      .setValue(
        this.languageGetModel.code != null
          ? this.languageGetModel.code
          : ""
      );
    this.labelImport.nativeElement.innerText =
      this.languageGetModel.fileName != null
        ? this.languageGetModel.fileName
        : "Choose file";
   
  }
  onFileChange(files: FileList) {
    this.labelImport.nativeElement.innerText = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
  }
  submitForm(): void {
    console.log('Hey')
    this.languageEditModel = {
      id: this.languageId,
      code: this.form.get("codeName").value,
      name: this.form.get("name").value,
      file: this.fileToUpload,
      fileName: this.labelImport.nativeElement.innerText
    };
    console.log(this.languageEditModel);
    if(this.isLanguageCreate){
      this.languageService
      .addLanguages(this.languageEditModel)
      .subscribe(response => {
        this.checkValidRequest(response["success"])
      });
    }else{
      this.languageService
      .editLanguages(this.languageEditModel)
      .subscribe(response => {
        this.checkValidRequest(response["success"])
      });
    }
     
  }
  cancelButton(): void {
    this.router.navigate(["/languageView"]);
  }
  private checkValidRequest(success:Boolean){
    if(success)
      this.router.navigate(["/languageView"]);
    else
      alert("Error");

  }
  
}
