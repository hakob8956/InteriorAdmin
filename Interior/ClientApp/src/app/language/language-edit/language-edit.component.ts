import { LanguageDataService } from './../../services/KendoCenter.service';
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
import { LanguageEditModel } from "src/app/models/Language";
import { LanguageService } from 'src/app/services/DataCenter.service';
@Component({
  selector: "app-language-edit",
  templateUrl: "./language-edit.component.html",
  styleUrls: ["./language-edit.component.scss"],
  providers:[LanguageService]
})
export class LanguageEditComponent implements OnInit {
  faSearch = faSearch;
  @ViewChild("labelImport")
  labelImport: ElementRef;
  languageEditModel: LanguageEditModel;
  form: FormGroup;
  fileToUpload: File = null;
  isLanguageCreate: boolean;
  languageId: number;
  constructor(private router: Router, private route: ActivatedRoute,private languageService:LanguageService) {}
  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl("", Validators.required),
      codeName: new FormControl("", Validators.required),
      importFile: new FormControl("")
    });
    this.languageId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.languageId) && this.languageId > 0) {
      this.isLanguageCreate = false;
      this.initForm();
    } else {
      this.languageId = 0;
      this.isLanguageCreate = true;
    }
  }
  initForm(){

  }
  onFileChange(files: FileList) {
    this.labelImport.nativeElement.innerText = Array.from(files)
      .map(f => f.name)
      .join(", ");
    this.fileToUpload = files.item(0);
  }
  submitForm(): void {
    console.log("import " + this.fileToUpload.name);
    this.languageEditModel= {
        code:this.form.get("codeName").value,
        name:this.form.get("name").value,
        file:this.fileToUpload,
        id:0
    }
    console.log(this.languageEditModel);

    this.languageService.addLanguages(this.languageEditModel).subscribe(response=>console.log(response));
  }
  cancelButton(): void {
    this.router.navigate(["/languageView"]);
  }
}
