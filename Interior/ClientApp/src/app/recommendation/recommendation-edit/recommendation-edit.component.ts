import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RecommendationService } from 'src/app/services/DataCenter.service';
import { RecommendationModel } from 'src/app/models/Recommendation';
import { ContentModel } from 'src/app/models/ContentModel';
import { FileModel } from 'src/app/models/File';
import { SectionType } from 'src/app/models/Enums';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-recommendation-edit',
  templateUrl: './recommendation-edit.component.html',
  styleUrls: ['./recommendation-edit.component.scss']
})
export class RecommendationEditComponent implements OnInit {

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private recommendationService: RecommendationService,
    private alertService:AlertService
  ) {}
  recommendationId: number;
  recommendationModel: RecommendationModel = new RecommendationModel();
  currentLanguageId: number;
  fileName:string="Choose File";
  mySectionType = SectionType;
  ngOnInit() {
    this.recommendationId = +this.route.snapshot.params["id"];
    if (!Number.isNaN(this.recommendationId) && this.recommendationId > 0) {
      this.recommendationService.getRecommendationbyId(this.recommendationId).subscribe(response => {
        this.recommendationModel = response["data"];
        console.log(this.recommendationModel)
        this.initForm();
      });
    } else {
      this.recommendationId = 0;
    }
  }

  initForm(){
    if (
      this.recommendationModel.currentFile != null &&
      this.recommendationModel.currentFile.fileName != null
    )
      this.fileName= this.recommendationModel.currentFile.fileName;
    else this.fileName= "Choose file";
    console.log(this.fileName);
  }
  changeContents(currentContents:ContentModel[]){
    this.recommendationModel.contents=currentContents; 
  }
  onFileChange(currentFile:any){
    this.recommendationModel.file=currentFile.file;
  }
  cancelButton() {
    this.router.navigate(["/recommendationView"]);
  }
  onChangeSelectionId(model){
    console.log(model)
    switch (model.type) {
      case this.mySectionType.Shop:
         this.recommendationModel.shopId = model.value;
         break;
      case this.mySectionType.Interior:
        this.recommendationModel.interiorId = model.value;
        break;
      case this.mySectionType.Category:
        this.recommendationModel.categoryId = model.value;
        break;
      case this.mySectionType.Brand:
        this.recommendationModel.brandId = model.value;
        break;
      default:
        break;
    }
  }
  submitForm() {
    if (this.recommendationModel.currentFile != null) {
      this.recommendationModel.currentFile.imageData = null;
      this.recommendationModel.currentFile.imageMimeType = null;
    } else {
      this.recommendationModel.currentFile = new FileModel();
    }

    this.alertService.clear();
    if (this.recommendationId == 0) {
      this.recommendationModel.id = 0;
      this.recommendationService
        .createRecommendation(this.recommendationModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    } else {
      this.recommendationService
        .editRecommendation(this.recommendationModel)
        .subscribe(response => this.checkValidRequest(response["success"]));
    }
  }
  private checkValidRequest(success: Boolean) {
    if (success){
      this.alertService.success("Success",true);
      this.router.navigate(["/recommendationView"]);
    } 
    else this.alertService.error("Error");
  }
}
