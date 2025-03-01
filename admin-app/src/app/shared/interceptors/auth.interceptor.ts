import { inject, Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpInterceptorFn, HttpHandlerFn } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { AuthService } from '../services';

@Injectable({
  providedIn: 'root'
})

export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {
    console.log('AuthInterceptor: constructor');
   }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authHeader = this.authService.authorizationHeaderValue;
    console.log('AuthInterceptor: Adding Authorization header', authHeader);
  
    if (authHeader) {
      request = request.clone({
        setHeaders: {
          Authorization: authHeader
        }
      });
    }
  
    return next.handle(request);
  }
}

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> => {
  
  const authService = inject(AuthService); // ✅ Inject AuthService

  const authHeader = authService.authorizationHeaderValue; 
  console.log('AuthInterceptor: Adding Authorization header', authHeader);

  if (authHeader) {
    req = req.clone({
      setHeaders: {
        Authorization: authHeader
      }
    });
  }

  return next(req).pipe(
    tap(event => {
      console.log('AuthInterceptor: Request processed');
    })
  );
};