import { InteriorRequestModel } from './../../models/Interior';
import { LanguageService } from 'src/app/services/DataCenter.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { InteriorService } from 'src/app/services/DataCenter.service';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LanguageModel } from 'src/app/models/Language';
import { Content } from '@angular/compiler/src/render3/r3_ast';

@Component({
  selector: 'app-interior-edit',
  templateUrl: './interior-edit.component.html',
  styleUrls: ['./interior-edit.component.scss'],
  providers:[InteriorService,LanguageService]
})
export class InteriorEditComponent implements OnInit {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private interiorService:InteriorService,
    private languageService:LanguageService) 
    { }
  @ViewChild("labelImport")
  labelImport: ElementRef;
  faSearch = faSearch;
  form: FormGroup;
  fileToUpload: File = null;
  interiorId: number;
    languageModel: LanguageModel;
  interiorGetModel:InteriorRequestModel=new InteriorRequestModel();
  contentsModel:Content[]=[];
  currentLanguageId:number;
  ngOnInit(): void {
    this.form = new FormGroup({
      price:new FormControl("",Validators.required)
    });
    this.languageService.getAllLanguages().subscribe(response => {
      this.languageModel = response;
      this.currentLanguageId = this.languageModel[0].id;
      console.log(this.currentLanguageId);
    });
    this.interiorId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.interiorId) && this.interiorId > 0) {
      this.interiorService.getInteriorbyId(this.interiorId).subscribe(response => {
        this.interiorGetModel = response["data"];
        console.log(this.interiorGetModel);
      });
    } else {
      this.interiorId = 0;
    }
    
  }



}