import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { BaseService } from './base.service';
import { catchError } from 'rxjs/operators';
import { User } from '../models';

@Injectable({ providedIn: 'root' })
export class FunctionService extends BaseService {

    constructor(private http: HttpClient) {
        super();
    }
    
    
}