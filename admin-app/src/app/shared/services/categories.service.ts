import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Pagination, Category } from '../models';

@Injectable({ providedIn: 'root' })
export class CategoriesService extends BaseService {
    private _sharedHeaders = new HttpHeaders();
    constructor(private http: HttpClient) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
    }

    add(entity: Category) {
        this.http.post(`${environment.apiUrl}/api/categories`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            .subscribe({
                next: (response) => {
                    console.log('Category added:', response);
                },
                error: (error) => {
                    console.error('Error adding category:', error);
                },
                complete: () => {
                    console.log('Adding category complete.');
                }
            });
    }

    update(id: number, entity: Category) {
        this.http.put(`${environment.apiUrl}/api/categories/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            .subscribe({
                next: (response) => {
                    console.log('Category updated:', response);
                },
                error: (error) => {
                    console.error('Error updating category:', error);
                },
                complete: () => {
                    console.log('Updating category complete.');
                }
            });
    }

    getDetail(id: number) {
        this.http.get<Category>(`${environment.apiUrl}/api/categories/${id}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            .subscribe({
                next: (response) => {
                    console.log('Category detail:', response);
                },
                error: (error) => {
                    console.error('Error fetching category detail:', error);
                },
                complete: () => {
                    console.log('Fetching category detail complete.');
                }
            });
    }

    getAllPaging(filter: string, pageIndex: number, pageSize: number) {
        this.http.get<Pagination<Category>>(`${environment.apiUrl}/api/categories/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}`, { headers: this._sharedHeaders })
            .pipe(
                map((response: Pagination<Category>) => {
                    return response;
                }),
                catchError(this.handleError)
            )
            .subscribe({
                next: (response) => {
                    console.log('Paged categories:', response);
                },
                error: (error) => {
                    console.error('Error fetching paged categories:', error);
                },
                complete: () => {
                    console.log('Fetching paged categories complete.');
                }
            });
    }

    delete(id: number) {
        this.http.delete(environment.apiUrl + '/api/categories/' + id, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError))
            .subscribe({
                next: (response) => {
                    console.log('Category deleted:', response);
                },
                error: (error) => {
                    console.error('Error deleting category:', error);
                },
                complete: () => {
                    console.log('Deleting category complete.');
                }
            });
    }

    getAll() {
        this.http.get<Category[]>(`${environment.apiUrl}/api/categories`, { headers: this._sharedHeaders })
            .pipe(
                map((response: Category[]) => {
                    return response;
                }),
                catchError(error => {
                    console.error('Error in getAll:', error);
                    return this.handleError(error);
                })
            )
            .subscribe({
                next: (categories) => {
                    console.log('Categories:', categories);
                },
                error: (error) => {
                    console.error('Error:', error);
                },
                complete: () => {
                    console.log('Fetching categories complete.');
                }
            });
    }
}