import { RegisterUserModel } from './../models/User';
import { map, tap, catchError } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { throwError, Observable, BehaviorSubject } from "rxjs";
import { HttpErrorResponse, HttpClient } from "@angular/common/http";
import { State, toODataString } from "@progress/kendo-data-query";

export abstract class BaseService{
  constructor(){}
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
  private BASE_URL = "https://localhost:44353/api";

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
  public UpdateUser(userModel:RegisterUserModel){
    return this.http.post(`${this.BASE_URL}/User/update-user`,userModel).pipe(
       catchError(this.handleError)
   );
}

}
@Injectable()
export class RoleService extends BaseService {
  constructor(private http: HttpClient) {super()}
  private BASE_URL = "https://localhost:44353/api";

  public getRoles():Observable<Array<any>>{
    return this.http.get(`${this.BASE_URL}/Role/get-roles`).pipe(
      map(reponse=> reponse["data"]),
      catchError(this.handleError)
    );
  }

  
}
