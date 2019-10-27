import { RegisterUserModel } from './../models/User';
import { map, tap, catchError } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { throwError, Observable, BehaviorSubject } from "rxjs";
import { HttpErrorResponse, HttpClient } from "@angular/common/http";
import { State, toODataString } from "@progress/kendo-data-query";

@Injectable()
export class UserService {
  constructor(private http: HttpClient) {}
  private BASE_URL = "https://localhost:44353/api";

  public getUserById(id: number) {
      console.log(id)
      console.log(`${this.BASE_URL}/User/get-byId/${id}`)
     return this.http.get(`${this.BASE_URL}/User/get-byId/${id}`);
  }
  public ChangeCreateUser(userModel:RegisterUserModel){
    console.log(userModel);
         return this.http.post(`${this.BASE_URL}/User/create-user`,userModel);
  }
  private handleError(err: HttpErrorResponse) {
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
