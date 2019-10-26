import { map, tap, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { throwError, Observable, BehaviorSubject } from 'rxjs';
import { HttpErrorResponse,HttpClient } from '@angular/common/http';
import { State, toODataString } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';

export abstract class  DataCenterService extends BehaviorSubject<GridDataResult>{
    public loading:boolean;
    private BASE_URL = 'https://localhost:44353';
    constructor(
        private http: HttpClient,
        protected tableName: string
    ) {
        super(null);
    }

    public query(state: any): void {
        this.fetch(this.tableName, state)
            .subscribe(x => super.next(x));
    }

    protected fetch(tableName: string, state: any): Observable<GridDataResult> {
        this.loading = true;
        console.log(state.sort)
        var dir = "";
        var field = "";
        if(state.sort.length > 0){
             dir=state.sort[0]["dir"];
             field=state.sort[0]["field"];
        }
        console.log(dir)
        
        return this.http
            .get(`${this.BASE_URL}/api/User/get-all-users`,{
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
export class AdminsService extends DataCenterService{
    constructor(http:HttpClient){super(http,'Admins');}
    queryAll(st?: any): Observable<GridDataResult> {
        const state = Object.assign({}, st);
        delete state.skip;
        delete state.take;

        return this.fetch(this.tableName, state);
    }
}