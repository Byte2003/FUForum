import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { Function, User } from '../models';
import { UtilitiesService } from './utilities.service';


@Injectable({ providedIn: 'root' })
export class UserService extends BaseService {

    constructor(private http: HttpClient, private utilitiesService: UtilitiesService) {
        console.log('UserService: constructor');
        super();
    }
    
    getAll() {
        const httpOption = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };
        const url = `${environment.apiUrl}/api/Users`;
        console.log('getAll URL:', url);
        
        return this.http.get<User[]>(url, httpOption)
            .pipe(
                catchError(error => {
                    console.error('Error in getAll:', error);
                    return this.handleError(error);
                }),
                map(response => {
                    console.log('getAll response:', response);
                    return response;
                })
            );
    }
    
    getMenuByUser(userId: string) {
        const httpOption = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        };
        const url = `${environment.apiUrl}/api/Users/${userId}/menu`;
        console.log('getMenuByUser URL:', url);
        return this.http.get<Function[]>(url, httpOption)
            .pipe(map(response => {
                var functions = this.utilitiesService.UnflatteringForLeftMenu(response);
                return functions;
            }));
        }       
}