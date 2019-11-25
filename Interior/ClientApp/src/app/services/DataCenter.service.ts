import { environment } from './../../environments/environment';
import { InteriorRequestModel } from "./../models/Interior";
import { GridDataResult } from "@progress/kendo-angular-grid";
import { CategoryEditModel } from "./../models/Category";
import { LanguageModel } from "src/app/models/Language";
import { LanguageEditComponent } from "./../Language/language-edit/language-edit.component";
import {
  RegisterUserModel,
  UpdateUserModel,
  ChangeUserPasswordModel
} from "./../models/User";
import { map, tap, catchError } from "rxjs/operators";
import { Injectable, OnInit } from "@angular/core";
import { throwError, Observable, BehaviorSubject } from "rxjs";
import { HttpErrorResponse, HttpClient } from "@angular/common/http";
import { State, toODataString } from "@progress/kendo-data-query";
import { ShopModel } from "../models/Shop";
import { BrandEditModel } from "../models/Brand";
import { RecommendationModel } from "../models/Recommendation";
import { AlertService } from './alert.service';




export abstract class BaseService {

  protected BASE_URL = environment.apiUrl;
  constructor(protected alertService:AlertService){}

  protected handleError(err: HttpErrorResponse) {
    let errorMessage = "";
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.log(errorMessage);
    this.alertService.error(errorMessage);
    return throwError(errorMessage);
  }
  
}

@Injectable()
export class UserService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }

  public getUserById(id: number) {
    return this.http
      .get(`${this.BASE_URL}/User/get-byId/${id}`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public CreateUser(userModel: RegisterUserModel) {
    return this.http
      .post(`${this.BASE_URL}/User/create-user`, userModel)
      .pipe(catchError(this.handleError.bind(this).bind(this)));
  }
  public UpdateUser(userModel: UpdateUserModel) {
    return this.http
      .put(`${this.BASE_URL}/User/update-user`, userModel)
      .pipe(catchError(this.handleError.bind(this).bind(this)));
  }
  public ChangePasswordUser(modelChange: ChangeUserPasswordModel) {
    return this.http
      .post(`${this.BASE_URL}/User/change-password`, modelChange)
      .pipe(catchError(this.handleError.bind(this).bind(this)));
  }
}
@Injectable()
export class RoleService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }

  public getRoles() {
    return this.http.get(`${this.BASE_URL}/Role/get-roles`).pipe(
      map(reponse => reponse["data"]),
      catchError(this.handleError.bind(this).bind(this))
    );
  }
}
@Injectable()
export class LanguageService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }

  public addLanguages(modelCreate: LanguageModel) {
    const formData: FormData = new FormData();
    if (modelCreate.file != null)
      formData.append("File", modelCreate.file, modelCreate.file.name);
    formData.append("Name", modelCreate.name);
    formData.append("Code", modelCreate.code);
    if (modelCreate.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(modelCreate.currentFile));
    formData.append("Id", modelCreate.id.toString());

    return this.http
      .post(`${this.BASE_URL}/Language/create-language`, formData)
      .pipe(catchError(this.handleError.bind(this).bind(this)));
  }
  public editLanguages(modelCreate: LanguageModel) {
    console.log(modelCreate);
    const formData: FormData = new FormData();
    if (modelCreate.file != null)
      formData.append("File", modelCreate.file, modelCreate.file.name);
    formData.append("Name", modelCreate.name);
    formData.append("Code", modelCreate.code);
    if (modelCreate.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(modelCreate.currentFile));
    formData.append("Id", modelCreate.id.toString());

    return this.http
      .post(`${this.BASE_URL}/Language/edit-language`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public getLanguage(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Language/get-byId/${id}`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public getAllLanguages() {
    return this.http.get(`${this.BASE_URL}/Language/get-all`).pipe(
      map(response => response["data"].data),
      catchError(this.handleError.bind(this))
    );
  }
}

@Injectable()
export class CategoryService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }
  public getCategoryAll(onlySubCategories:any=false,onlyCategories:any=false) {
    return this.http
      .get(`${this.BASE_URL}/Category/get-all`,{
        params:{
            onlySubCategories:onlySubCategories,
            onlyCategories:onlyCategories
        },
      }).pipe(catchError(this.handleError.bind(this)));
  }
  public getCategory(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Category/get-byId/${id}`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public createCategory(model: CategoryEditModel) {
    //this.test("test");
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Category/create-category`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public editCategory(model: CategoryEditModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Category/edit-category`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
}
@Injectable({
  providedIn: "root"
})
export class ShopService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }
  public getShopAll() {
    return this.http
      .get(`${this.BASE_URL}/Shop/get-all`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public getShopbyId(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Shop/get-byId/${id}`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public createShop(model: ShopModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Shop/create-shop`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public editShop(model: ShopModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Shop/edit-shop`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
}
@Injectable({
  providedIn: "root"
})
export class BrandService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }
  public getBrandAll() {
    return this.http
      .get(`${this.BASE_URL}/Brand/get-all`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public getBrandbyId(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Brand/get-byId/${id}`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public createBrand(model: BrandEditModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile)); //old file
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Brand/create-brand`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public editBrand(model: BrandEditModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Brand/edit-brand`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
}
@Injectable({
  providedIn: "root"
})
export class InteriorService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }
  public getInteriorbyId(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Interior/get-byId/${id}`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public getAllInterior() {
    return this.http
      .get(`${this.BASE_URL}/Interior/get-all`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public createInterior(model: InteriorRequestModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.androidFile != null)
      formData.append("AndroidFile", model.androidFile, model.androidFile.name);
    if (model.iosFile != null)
      formData.append("IosFile", model.iosFile, model.iosFile.name);
    if (model.glbFile != null)
      formData.append("GlbFile", model.glbFile, model.glbFile.name);
    if (model.imageFile != null)
      formData.append("ImageFile", model.imageFile, model.imageFile.name);
    if (model.nameContent)
      formData.append("NameContent", JSON.stringify(model.nameContent));
    if (model.descriptionContent)
      formData.append(
        "DescriptionContent",
        JSON.stringify(model.descriptionContent)
      );
    formData.append("Price", model.price.toString());
    formData.append("IsAvailable", model.isAvailable.toString());
    if (model.isVisible)
      formData.append("IsVisible", model.isVisible.toString());
    if (model.buyUrl) 
        formData.append("BuyUrl", model.buyUrl.toString());
    formData.append("ShopId", model.shopId.toString());
    formData.append("BrandId", model.brandId.toString());
    formData.append("CategoryId", model.categoryId.toString());
    if (model.optionContents)
      formData.append("OptionContents", JSON.stringify(model.optionContents));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Interior/create-interior`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public editInterior(model: InteriorRequestModel) {
    const formData: FormData = new FormData();
    if (model.androidFile != null)
      formData.append("AndroidFile", model.androidFile, model.androidFile.name);
    if (model.iosFile != null)
      formData.append("IosFile", model.iosFile, model.iosFile.name);
    if (model.glbFile != null)
      formData.append("GlbFile", model.glbFile, model.glbFile.name);
    if (model.imageFile != null)
      formData.append("ImageFile", model.imageFile, model.imageFile.name);
    if (model.nameContent)
      formData.append("NameContent", JSON.stringify(model.nameContent));
    if (model.descriptionContent)
      formData.append(
        "DescriptionContent",
        JSON.stringify(model.descriptionContent)
      );
    formData.append("Price", model.price.toString());
    formData.append("IsAvailable", model.isAvailable.toString());
    if (model.isVisible)
      formData.append("IsVisible", model.isVisible.toString());
    if (model.buyUrl) 
      formData.append("BuyUrl", model.buyUrl.toString());
    formData.append("ShopId", model.shopId.toString());
    formData.append("BrandId", model.brandId.toString());
    formData.append("CategoryId", model.categoryId.toString());
    if (model.optionContents)
      formData.append("OptionContents", JSON.stringify(model.optionContents));
    if (model.fileIdStorage)
      formData.append("FileIdStorage", JSON.stringify(model.fileIdStorage));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Interior/edit-interior`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
}

@Injectable({
  providedIn: "root"
})
export class RecommendationService extends BaseService {
  constructor(private http: HttpClient,protected alertService:AlertService) {
    super(alertService);
  }
  public getRecommendationbyId(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Recommendation/get-byId/${id}`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public getAllRecommendation() {
    return this.http
      .get(`${this.BASE_URL}/Recommendation/get-all`)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public createRecommendation(model: RecommendationModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile)); //old file
    formData.append("ShopId", model.shopId.toString());
    formData.append("InteriorId", model.interiorId.toString());
    formData.append("CategoryId", model.categoryId.toString());
    formData.append("BrandId", model.brandId.toString());

    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Recommendation/create-recommendation`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
  public editRecommendation(model: RecommendationModel) {
    console.log(model);
    const formData: FormData = new FormData();
    if (model.file != null)
      formData.append("File", model.file, model.file.name);
    formData.append("Contents", JSON.stringify(model.contents));
    if (model.currentFile != null)
      formData.append("CurrentFile", JSON.stringify(model.currentFile));
    formData.append("ShopId", model.shopId.toString());
    formData.append("InteriorId", model.interiorId.toString());
    formData.append("CategoryId", model.categoryId.toString());
    formData.append("BrandId", model.brandId.toString());
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Recommendation/edit-recommendation`, formData)
      .pipe(catchError(this.handleError.bind(this)));
  }
}
