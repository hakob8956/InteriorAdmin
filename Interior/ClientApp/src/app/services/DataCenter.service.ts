import { CategoryEditModel } from './../models/Category';
import { LanguageEditModel } from 'src/app/models/Language';
import { LanguageEditComponent } from './../Language/language-edit/language-edit.component';
import { RegisterUserModel, UpdateUserModel, ChangeUserPasswordModel } from './../models/User';
import { map, tap, catchError } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { throwError, Observable, BehaviorSubject } from "rxjs";
import { HttpErrorResponse, HttpClient } from "@angular/common/http";
import { State, toODataString } from "@progress/kendo-data-query";
import { ShopEditModel } from '../models/Shop';
import { BrandEditModel } from '../models/Brand';

export abstract class BaseService{
  constructor(){}
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
  constructor(private http: HttpClient) {super()}

  public getUserById(id: number) {
     return this.http.get(`${this.BASE_URL}/User/get-byId/${id}`).pipe(
       catchError(this.handleError)
     );
  }
  public CreateUser(userModel:RegisterUserModel){
         return this.http.post(`${this.BASE_URL}/User/create-user`,userModel).pipe(
          catchError(this.handleError)
        );
  }
  public UpdateUser(userModel:UpdateUserModel){
    return this.http.put(`${this.BASE_URL}/User/update-user`,userModel).pipe(
       catchError(this.handleError)
   );
  }
  public ChangePasswordUser(modelChange:ChangeUserPasswordModel){
     return this.http.post(`${this.BASE_URL}/User/change-password`,modelChange).pipe(
       catchError(this.handleError)
     );
  }
}
@Injectable()
export class RoleService extends BaseService {
  constructor(private http: HttpClient) {super()}

  public getRoles(){
    return this.http.get(`${this.BASE_URL}/Role/get-roles`).pipe(
      map(reponse=> reponse["data"]),
      catchError(this.handleError)
    );
  } 
}
@Injectable()
export class LanguageService extends BaseService {
  constructor(private http: HttpClient) {super()}
  
  public addLanguages(modelCreate:LanguageEditModel){
    const formData:FormData = new FormData();
    if(modelCreate.file != null)
        formData.append('File',modelCreate.file,modelCreate.file.name);
    formData.append('Name',modelCreate.name);
    formData.append('Code',modelCreate.code);
    if(modelCreate.fileName != null)
        formData.append('FileName',modelCreate.fileName);
    formData.append('Id',modelCreate.id.toString());

    return this.http.post(`${this.BASE_URL}/Language/create-language`,formData).pipe(
      catchError(this.handleError)
    );
  } 
  public editLanguages(modelCreate:LanguageEditModel){
    console.log(modelCreate)
    const formData:FormData = new FormData();
    if(modelCreate.file != null)
        formData.append('File',modelCreate.file,modelCreate.file.name);
    formData.append('Id',modelCreate.id.toString());
    formData.append('Name',modelCreate.name);
    formData.append('Code',modelCreate.code);
    formData.append('FileName',modelCreate.fileName);

    return this.http.post(`${this.BASE_URL}/Language/edit-language`,formData).pipe(
      catchError(this.handleError)
    );
  } 
  public getLanguage(id:number){
    return this.http.get(`${this.BASE_URL}/Language/get-byId/${id}`).pipe(
      catchError(this.handleError)
    );
    
  }
  public getAllLanguages(){
    return this.http.get(`${this.BASE_URL}/Language/get-all`).pipe(
      map(response=>response["data"].data),
      catchError(this.handleError)
    ); 
}
}

@Injectable()
export class CategoryService extends BaseService {
  constructor(private http: HttpClient) {super()}
  public getCategory(id:number){
    return this.http.get(`${this.BASE_URL}/Category/get-byId/${id}`).pipe(
      catchError(this.handleError)
    );
  }
  public createCategory(model:CategoryEditModel){
    console.log(model)
    const formData:FormData = new FormData();
    if(model.file != null)
        formData.append('File',model.file,model.file.name);
    formData.append('Contents',JSON.stringify(model.contents));
    formData.append('FileName',model.fileName);
    formData.append('Id',model.id.toString());
    return this.http.post(`${this.BASE_URL}/Category/create-category`,formData).pipe(
      catchError(this.handleError)
    );
  }
  public editCategory(model:CategoryEditModel){
    console.log(model)
    const formData:FormData = new FormData();
    if(model.file != null)
        formData.append('File',model.file,model.file.name);
    formData.append('Contents',JSON.stringify(model.contents));
    formData.append('FileName',model.fileName);
    formData.append('Id',model.id.toString());
    return this.http.post(`${this.BASE_URL}/Category/edit-category`,formData).pipe(
      catchError(this.handleError)
    );
  }
}
@Injectable({
  providedIn: 'root'
})
export class ShopService extends BaseService {
  constructor(private http: HttpClient) {super()}
  public getShopbyId(id:number){
    return this.http.get(`${this.BASE_URL}/Shop/get-byId/${id}`).pipe(
      catchError(this.handleError)
    );
  }
  public createShop(model:ShopEditModel){
    console.log(model)
    const formData:FormData = new FormData();
    if(model.file != null)
        formData.append('File',model.file,model.file.name);
    formData.append('Contents',JSON.stringify(model.contents));
    formData.append('FileName',model.fileName);
    formData.append('Id',model.id.toString());
    return this.http.post(`${this.BASE_URL}/Shop/create-shop`,formData).pipe(
      catchError(this.handleError)
    );
  }
  public editShop(model:ShopEditModel){
    console.log(model)
    const formData:FormData = new FormData();
    if(model.file != null)
        formData.append('File',model.file,model.file.name);
    formData.append('Contents',JSON.stringify(model.contents));
    formData.append('FileName',model.fileName);
    formData.append('Id',model.id.toString());
    return this.http.post(`${this.BASE_URL}/Shop/edit-shop`,formData).pipe(
      catchError(this.handleError)
    );
  }
}
@Injectable({
    providedIn: 'root'
})
export class BrandService extends BaseService {
    constructor(private http: HttpClient) { super() }
    public getBrandbyId(id: number) {
        return this.http.get(`${this.BASE_URL}/Brand/get-byId/${id}`).pipe(
            catchError(this.handleError)
        );
    }
    public createBrand(model: BrandEditModel) {
        console.log(model)
        const formData: FormData = new FormData();
        if (model.file != null)
            formData.append('File', model.file, model.file.name);
        formData.append('Contents', JSON.stringify(model.contents));
        formData.append('FileName', model.fileName);
        formData.append('Id', model.id.toString());
        return this.http.post(`${this.BASE_URL}/Brand/create-brand`, formData).pipe(
            catchError(this.handleError)
        );
    }
    public editBrand(model: BrandEditModel) {
        console.log(model)
        const formData: FormData = new FormData();
        if (model.file != null)
            formData.append('File', model.file, model.file.name);
        formData.append('Contents', JSON.stringify(model.contents));
        formData.append('FileName', model.fileName);
        formData.append('Id', model.id.toString());
        return this.http.post(`${this.BASE_URL}/Brand/edit-brand`, formData).pipe(
            catchError(this.handleError)
        );
    }
}
@Injectable({
  providedIn: 'root'
})
export class InteriorService extends BaseService {
  constructor(private http: HttpClient) { super() }
  public getInteriorbyId(id: number) {
      return this.http.get(`${this.BASE_URL}/Interior/get-byId/${id}`).pipe(
          catchError(this.handleError)
      );
  }
  // public createBrand(model: BrandEditModel) {
  //     console.log(model)
  //     const formData: FormData = new FormData();
  //     if (model.file != null)
  //         formData.append('File', model.file, model.file.name);
  //     formData.append('Contents', JSON.stringify(model.contents));
  //     formData.append('FileName', model.fileName);
  //     formData.append('Id', model.id.toString());
  //     return this.http.post(`${this.BASE_URL}/Brand/create-brand`, formData).pipe(
  //         catchError(this.handleError)
  //     );
  // }
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
