import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, config } from 'rxjs';
import { map } from 'rxjs/operators';
import { LoginUserModel } from '../models/User';
import { environment } from 'src/environments/environment';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<LoginUserModel>;
    public currentUser: Observable<LoginUserModel>;
    protected BASE_URL = environment.apiUrl;


    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<LoginUserModel>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): LoginUserModel {
        return this.currentUserSubject.value;
    }

    login(model:LoginUserModel) {
        console.log(model)
        return this.http.post<any>(`${this.BASE_URL}/user/authenticate`, model)
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user["data"]));
                this.currentUserSubject.next(user["data"]);
                return user["data"];
            }));
    }

    logout() {
        // remove user from local storage and set current user to null
         localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}