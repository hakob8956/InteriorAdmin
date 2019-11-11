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
import { LanguageModel } from "src/app/models/Language";
import { LanguageService } from "src/app/services/DataCenter.service";
import { DomSanitizer } from '@angular/platform-browser';
import { FileModel } from 'src/app/models/File';

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
  languageModel: LanguageModel = new LanguageModel();
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
        this.languageModel = response["data"];
        this.initForm();
      });
      
    } else {
      this.languageId = 0;
      this.isLanguageCreate = true;
    }
  }
  initForm() {
    console.log(this.languageModel)
    this.form
      .get("name")
      .setValue(
        this.languageModel.name != null
          ? this.languageModel.name
          : ""
      );
    this.form
      .get("codeName")
      .setValue(
        this.languageModel.code != null
          ? this.languageModel.code
          : ""
      );
      if (
        this.languageModel.currentFile != null &&
        this.languageModel.currentFile.fileName != null
      )
        this.labelImport.nativeElement.innerText = this.languageModel.currentFile.fileName;
      else this.labelImport.nativeElement.innerText = "Choose file";
   
  }
  onFileChange(files: FileList) {
    this.labelImport.nativeElement.innerText = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
  }
  submitForm(): void {
    this.languageModel.file = this.fileToUpload;
    if (this.languageModel.currentFile != null) {
      this.languageModel.currentFile.imageData = null;
      this.languageModel.currentFile.imageMimeType = null;
    } else {
      this.languageModel.currentFile = new FileModel();
    }
    this.languageModel.currentFile.fileName = this.labelImport.nativeElement.innerText;
    this.languageModel.name=this.form.get("name").value;
    this.languageModel.code=this.form.get("codeName").value;

    if(this.isLanguageCreate){
      this.languageModel.id=0;
      this.languageService
      .addLanguages(this.languageModel)
      .subscribe(response => {
        this.checkValidRequest(response["success"])
      });
    }else{
      this.languageService
      .editLanguages(this.languageModel)
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
