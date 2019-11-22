import { AlertService } from './../../services/alert.service';
import { Router, ActivatedRoute } from "@angular/router";
import {
  Component,
  OnInit,
  ElementRef,
  EventEmitter,
  ViewChild
} from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { LanguageModel } from "src/app/models/Language";
import { LanguageService } from "src/app/services/DataCenter.service";
import { FileModel } from 'src/app/models/File';

@Component({
  selector: "app-language-edit",
  templateUrl: "./language-edit.component.html",
  styleUrls: ["./language-edit.component.scss"],
  providers: [LanguageService]
})
export class LanguageEditComponent implements OnInit {

  languageModel: LanguageModel = new LanguageModel();
  form: FormGroup;
  isLanguageCreate: boolean;
  languageId: number;
  imageSource:any;
  fileName:string;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private languageService: LanguageService,
    private alertService:AlertService
  ) {}
  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl("", Validators.required),
      codeName: new FormControl("", Validators.required),
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
  }
  get getFile(){
    if(this.languageModel && this.languageModel.currentFile)
        return this.languageModel.currentFile;
    return null;
  }
  onFileChange(currentFile:any){
    this.languageModel.file=currentFile.file;
  }
  submitForm(): void {
    if (this.languageModel.currentFile != null) {
      this.languageModel.currentFile.imageData = null;
      this.languageModel.currentFile.imageMimeType = null;
    } else {
      this.languageModel.currentFile = new FileModel();
    }
    this.alertService.clear();
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
