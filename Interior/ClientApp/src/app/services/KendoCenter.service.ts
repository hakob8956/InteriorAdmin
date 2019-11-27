import { map, tap, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { throwError, Observable, BehaviorSubject } from 'rxjs';
import { HttpErrorResponse,HttpClient } from '@angular/common/http';
import { State, toODataString } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { RegisterUserModel } from '../models/User';
import { AlertService } from './alert.service';
import { environment } from 'src/environments/environment';

export abstract class  KendoCenterService extends BehaviorSubject<GridDataResult>{
    public loading:boolean;
    private BASE_URL = environment.apiUrl;
    constructor(
        private http: HttpClient,
        private alertSerice:AlertService,
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
        var onlySubCategories=state.onlySubCategories?state.onlySubCategories:false;
        var onlyCategories=state.onlyCategories?state.onlyCategories:false;
        if(state.sort && state.sort.length > 0){
             dir=state.sort[0]["dir"];
             field=state.sort[0]["field"];
        }
        return this.http
            .get(`${this.BASE_URL}/${tableName}/get-all`,{
                params:{
                    skip:state.skip,
                    take:state.take,
                    dir:dir,
                    field:field,
                    onlySubCategories:onlySubCategories,
                    onlyCategories:onlyCategories
                },
            })
            .pipe(
                map(response => (<GridDataResult>{
                    data: response['data'].data,
                    total: parseInt(response['data'].lenght, 10),
                })),
                tap(() => this.loading = false),
                catchError(this.handleError.bind(this))
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
                catchError(this.handleError.bind(this))
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
        this.alertSerice.error(errorMessage);
        return throwError(errorMessage);
    }
}
@Injectable()
export class UserDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,null,'User');}
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
    constructor(http:HttpClient){super(http,null,'Language');}
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
    constructor(http:HttpClient){super(http,null,'Category');}
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
    constructor(http:HttpClient){super(http,null,'Shop');}
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
    constructor(http:HttpClient){super(http,null,'Brand');}
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
    constructor(http:HttpClient){super(http,null,'Interior');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }
}
@Injectable()
export class RecommendationDataService extends KendoCenterService{
    constructor(http:HttpClient){super(http,null,'Recommendation');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;
        delete state.sort;
        return this.fetch(this.tableName, state);
    }
}

