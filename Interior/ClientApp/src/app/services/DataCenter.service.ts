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
import { Injectable } from "@angular/core";
import { throwError, Observable, BehaviorSubject } from "rxjs";
import { HttpErrorResponse, HttpClient } from "@angular/common/http";
import { State, toODataString } from "@progress/kendo-data-query";
import { ShopModel } from "../models/Shop";
import { BrandEditModel } from "../models/Brand";

export abstract class BaseService {
  constructor() {}
  protected BASE_URL = "https://localhost:44353/api";
  protected handleError(err: HttpErrorResponse) {
    let errorMessage = "";
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}

@Injectable()
export class UserService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }

  public getUserById(id: number) {
    return this.http
      .get(`${this.BASE_URL}/User/get-byId/${id}`)
      .pipe(catchError(this.handleError));
  }
  public CreateUser(userModel: RegisterUserModel) {
    return this.http
      .post(`${this.BASE_URL}/User/create-user`, userModel)
      .pipe(catchError(this.handleError));
  }
  public UpdateUser(userModel: UpdateUserModel) {
    return this.http
      .put(`${this.BASE_URL}/User/update-user`, userModel)
      .pipe(catchError(this.handleError));
  }
  public ChangePasswordUser(modelChange: ChangeUserPasswordModel) {
    return this.http
      .post(`${this.BASE_URL}/User/change-password`, modelChange)
      .pipe(catchError(this.handleError));
  }
}
@Injectable()
export class RoleService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }

  public getRoles() {
    return this.http.get(`${this.BASE_URL}/Role/get-roles`).pipe(
      map(reponse => reponse["data"]),
      catchError(this.handleError)
    );
  }
}
@Injectable()
export class LanguageService extends BaseService {
  constructor(private http: HttpClient) {
    super();
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
      .pipe(catchError(this.handleError));
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
      .pipe(catchError(this.handleError));
  }
  public getLanguage(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Language/get-byId/${id}`)
      .pipe(catchError(this.handleError));
  }
  public getAllLanguages() {
    return this.http.get(`${this.BASE_URL}/Language/get-all`).pipe(
      map(response => response["data"].data),
      catchError(this.handleError)
    );
  }
}

@Injectable()
export class CategoryService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }
  public getCategoryAll() {
    return this.http
      .get(`${this.BASE_URL}/Category/get-all`)
      .pipe(catchError(this.handleError));
  }
  public getCategory(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Category/get-byId/${id}`)
      .pipe(catchError(this.handleError));
  }
  public createCategory(model: CategoryEditModel) {
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
      .pipe(catchError(this.handleError));
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
      .pipe(catchError(this.handleError));
  }
}
@Injectable({
  providedIn: "root"
})
export class ShopService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }
  public getShopAll() {
    return this.http
      .get(`${this.BASE_URL}/Shop/get-all`)
      .pipe(catchError(this.handleError));
  }
  public getShopbyId(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Shop/get-byId/${id}`)
      .pipe(catchError(this.handleError));
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
      .pipe(catchError(this.handleError));
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
      .pipe(catchError(this.handleError));
  }
}
@Injectable({
  providedIn: "root"
})
export class BrandService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }
  public getBrandAll() {
    return this.http
      .get(`${this.BASE_URL}/Brand/get-all`)
      .pipe(catchError(this.handleError));
  }
  public getBrandbyId(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Brand/get-byId/${id}`)
      .pipe(catchError(this.handleError));
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
      .pipe(catchError(this.handleError));
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
      .pipe(catchError(this.handleError));
  }
}
@Injectable({
  providedIn: "root"
})
export class InteriorService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }
  public getInteriorbyId(id: number) {
    return this.http
      .get(`${this.BASE_URL}/Interior/get-byId/${id}`)
      .pipe(catchError(this.handleError));
  }
  public getAllInterior() {
    return this.http
      .get(`${this.BASE_URL}/Interior/get-all`)
      .pipe(catchError(this.handleError));
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
    if (model.buyUrl) formData.append("BuyUrl", model.buyUrl.toString());
    formData.append("ShopId", model.shopId.toString());
    formData.append("BrandId", model.brandId.toString());
    formData.append("CategoryId", model.categoryId.toString());
    if (model.optionContents)
      formData.append("OptionContents", JSON.stringify(model.optionContents));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Interior/create-interior`, formData)
      .pipe(catchError(this.handleError));
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
    if (model.buyUrl) formData.append("BuyUrl", model.buyUrl.toString());
    formData.append("ShopId", model.shopId.toString());
    formData.append("BrandId", model.brandId.toString());
    formData.append("CategoryId", model.categoryId.toString());
    if (model.optionContents)
      formData.append("OptionContents", JSON.stringify(model.optionContents));
    if(model.fileIdStorage)
        formData.append("FileIdStorage", JSON.stringify(model.fileIdStorage));
    formData.append("Id", model.id.toString());
    return this.http
      .post(`${this.BASE_URL}/Interior/edit-interior`, formData)
      .pipe(catchError(this.handleError));
  }
  // public editBrand(model: BrandEditModel) {
  //     console.log(model)
  //     const formData: FormData = new FormData();
  //     if (model.file != null)
  //         formData.append('File', model.file, model.file.name);
  //     formData.append('Contents', JSON.stringify(model.contents));
  //     formData.append('FileName', model.fileName);
  //     formData.append('Id', model.id.toString());
  //     return this.http.post(`${this.BASE_URL}/Brand/edit-brand`, formData).pipe(
  //         catchError(this.handleError)
  //     );
  // }
}
