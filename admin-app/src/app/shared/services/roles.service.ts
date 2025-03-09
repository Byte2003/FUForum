import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Role } from '../models';
import { Pagination, Permission } from '../models';


@Injectable({ providedIn: 'root' })
export class RolesService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }
    add(entity: Role) {
        return this.http.post(`${environment.apiUrl}/api/roles`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            // .subscribe({
            //     next: (response) => {
            //         console.log('Role added:', response);
            //     },
            //     error: (error) => {
            //         console.error('Error adding role:', error);
            //     },
            //     complete: () => {
            //         console.log('Adding role complete.');
            //     }
            // });
    }

    update(id: string, entity: Role) {
        return this.http.put(`${environment.apiUrl}/api/roles/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            // .subscribe({
            //     next: (response) => {
            //         console.log('Role updated:', response);
            //     },
            //     error: (error) => {
            //         console.error('Error updating role:', error);
            //     },
            //     complete: () => {
            //         console.log('Updating role complete.');
            //     }
            // });
    }

    getDetail(id: string) {
        return this.http.get<Role>(`${environment.apiUrl}/api/roles/${id}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            // .subscribe({
            //     next: (response) => {
            //         console.log('Role detail:', response);
            //     },
            //     error: (error) => {
            //         console.error('Error fetching role detail:', error);
            //     },
            //     complete: () => {
            //         console.log('Fetching role detail complete.');
            //     }
            // });
    }

    getAllPaging(filter:any, pageIndex: number, pageSize: number) {
        return this.http.get<Pagination<Role>>(`${environment.apiUrl}/api/roles/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}`, { headers: this._sharedHeaders })
            .pipe(map((response: Pagination<Role>) => {
                return response;
            }), catchError(this.handleError))
            // .subscribe({
            //     next: (response) => {
            //         console.log('Roles:', response);
            //     },
            //     error: (error) => {
            //         console.error('Error fetching roles:', error);
            //     },
            //     complete: () => {
            //         console.log('Fetching roles complete.');
            //     }
            // });
    }

    delete(id: string) {
        return this.http.delete(environment.apiUrl + '/api/roles/' + id, { headers: this._sharedHeaders })
            .pipe(
                catchError(this.handleError)
            )
            // .subscribe({
            //     next: (response) => {
            //         console.log('Role deleted:', response);
            //     },
            //     error: (error) => {
            //         console.error('Error deleting role:', error);
            //     },
            //     complete: () => {
            //         console.log('Deleting role complete.');
            //     }
            // });
    }

    getAll() {
        return this.http.get<Role[]>(`${environment.apiUrl}/api/roles`, { headers: this._sharedHeaders })
            .pipe(map((response: Role[]) => {
                return response;
            }), catchError(this.handleError))
            // .subscribe({
            //     next: (response) => {
            //         console.log('Roles:', response);
            //     },
            //     error: (error) => {
            //         console.error('Error fetching roles:', error);
            //     },
            //     complete: () => {
            //         console.log('Fetching roles complete.');
            //     }
            // });
    }
    getRolePermissions(roleId: string) {
        return this.http.get<Permission[]>(`${environment.apiUrl}/api/roles/${roleId}/permissions`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            // .subscribe({
            //     next: (response) => {
            //         console.log('Role permissions:', response);
            //     },
            //     error: (error) => {
            //         console.error('Error fetching role permissions:', error);
            //     },
            //     complete: () => {
            //         console.log('Fetching role permissions complete.');
            //     }
            // });
    }
}