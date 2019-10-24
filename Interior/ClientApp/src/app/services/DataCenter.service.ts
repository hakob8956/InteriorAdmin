import { map, tap, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { throwError, Observable, BehaviorSubject } from 'rxjs';
import { HttpErrorResponse,HttpClient } from '@angular/common/http';
import { State } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';
@Injectable()
export class DataCenterService extends BehaviorSubject<GridDataResult>{
    private BaseUrl = window.location.protocol + '//' + window.location.host;
    constructor(private http: HttpClient) {super(null)}

    public getUserReviewInfos(state:State){
         return this.http.get(this.BaseUrl+'/api/User/get-users',
         {params:{skip:String(state.skip),take:String(state.take)}})
         .pipe(
             map(response=> (<GridDataResult>{
                data: response['data'],
                total: 1
                })),
            catchError(this.handleError)
            ).subscribe(response=>super.next(response));
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