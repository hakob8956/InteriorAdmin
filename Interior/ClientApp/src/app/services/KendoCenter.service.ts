import { map, tap, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { throwError, Observable, BehaviorSubject } from 'rxjs';
import { HttpErrorResponse,HttpClient } from '@angular/common/http';
import { State, toODataString } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { RegisterUserModel } from '../models/User';

export abstract class  KendoCenterService extends BehaviorSubject<GridDataResult>{
    public loading:boolean;
    private BASE_URL = 'https://localhost:44353/api';
    constructor(
        private http: HttpClient,
        protected tableName: string
    ) {
        super(null);
    }

    public query(state: any): void {
        if(state==null){
            this.fetchWithoutState(this.tableName)
            .subscribe(x => super.next(x));
        }else{
            this.fetch(this.tableName, state)
            .subscribe(x => super.next(x));
        }
       
    }

    protected fetch(tableName: string, state: any): Observable<GridDataResult> {
        this.loading = true;
        var dir = "";
        var field = "";
        if(state.sort.length > 0){
             dir=state.sort[0]["dir"];
             field=state.sort[0]["field"];
        }
        return this.http
            .get(`${this.BASE_URL}/${tableName}/get-all`,{
                params:{
                    skip:state.skip,
                    take:state.take,
                    dir:dir,
                    field:field
                },
            })
            .pipe(
                map(response => (<GridDataResult>{
                    data: response['data'].data,
                    total: parseInt(response['data'].lenght, 10),
                })),
                tap(() => this.loading = false),
                catchError(this.handleError)
            );
    }
    protected fetchWithoutState(tableName: string): Observable<GridDataResult> {
        this.loading = true;
        return this.http
            .get(`${this.BASE_URL}/${tableName}/get-all`)
            .pipe(
                map(response => (<GridDataResult>{
                    data: response['data'].data,
                    total: parseInt(response['data'].lenght, 10),
                })),
                tap(() => this.loading = false),
                catchError(this.handleError)
            );
    }

    private handleError(err: HttpErrorResponse) {
 
        let errorMessage = '';
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
export class UserDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,'User');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }

}
@Injectable()
export class LanguageDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,'Language');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }

}
@Injectable()
export class CategoryDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,'Category');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }
}
@Injectable()
export class ShopDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,'Shop');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }
}
@Injectable()
export class BrandDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,'Brand');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }
}
@Injectable()
export class InteriorDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,'Interior');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }
}

