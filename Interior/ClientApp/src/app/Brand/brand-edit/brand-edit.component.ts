import { LanguageService, BrandService } from './../../services/DataCenter.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { Form, FormGroup, FormControl, Validators } from '@angular/forms';
import { BrandEditModel } from 'src/app/models/Brand';
import { LanguageGetModel } from 'src/app/models/Language';
import { Content } from 'src/app/models/Content';
import { FileModel } from 'src/app/models/File';


@Component({
    selector: 'app-brand-edit',
    templateUrl: './brand-edit.component.html',
    styleUrls: ['./brand-edit.component.scss'],
    providers: [BrandService, LanguageService]
})
export class BrandEditComponent implements OnInit {

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private languageService: LanguageService,
        private brandService: BrandService
    ) { }

    @ViewChild("labelImport")
    labelImport: ElementRef;
    faSearch = faSearch;
    fileToUpload: File = null;
    brandId: number;
    form: FormGroup;
    languageModel: LanguageGetModel;
    brandModel: BrandEditModel = new BrandEditModel();
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
        this.brandId = +this.route.snapshot.params["id"];
        if (!Number.isNaN(this.brandId) && this.brandId > 0) {
            this.brandService.getBrandbyId(this.brandId).subscribe(response => {
                this.brandModel = response["data"];
                console.log(this.brandModel);
                this.initForm();
            });
        } else {
            this.brandId = 0;
        }
    }
    initForm() {
        //console.log(this.languageModel[0].name);
        // this.form
        //   .get("language")
        //   .setValue(
        //     this.languageModel[0].name != null ? this.languageModel[0].name : ""
        //   );
        console.log(this.brandModel.currentFile)
        this.labelImport.nativeElement.innerText =
            this.brandModel.currentFile.fileName != null
                ? this.brandModel.currentFile.fileName
                : "Choose file";
    }
    inputTextChange(element: any) {
        if(this.brandModel.contents != null)
            this.contentsModel = this.brandModel.contents;
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
        this.brandModel.contents=this.contentsModel;
        console.log(this.brandModel);
      }
      getCurrentIdFromContentModel(languageId:number):number{
        let result:number = 0;
        if (this.brandId > 0)
        {
          this.brandModel.contents.forEach(el=>{
            if(languageId == el.languageId){
                result=el.id;
            }
          });
        }
        return result;
        
      }
    getTextFromBrand(languageId: number): string {
        let output: string = "";
        if (this.brandId == 0) {
            return output;
        }
        try {
            this.brandModel.contents.forEach(element => {
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
        this.router.navigate(["/brandView"]);
    }
    submitForm() {
        this.brandModel.file = this.fileToUpload;
        // console.log(this.brandModel);
        if (this.brandId == 0) {
            this.brandModel.id = 0;
            this.brandModel.currentFile = new FileModel();
            this.brandModel.currentFile.fileName =  this.labelImport.nativeElement.innerText;

            this.brandService
                .createBrand(this.brandModel)
                .subscribe(response => this.checkValidRequest(response["success"]));
        } else {
            this.brandModel.currentFile.imageData=null;
            this.brandModel.currentFile.imageMimeType=null;
            this.brandModel.currentFile.fileName = this.labelImport.nativeElement.innerText;
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

