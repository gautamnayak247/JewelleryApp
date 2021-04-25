import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private userService: UserService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let token = this.userService.GetToken();
        if (token !== undefined) {
            request = request.clone({
                setHeaders: { 
                    'X-Token': `${token}`
                }
            });
        }
        //setting up content negotiation header
        request = request.clone({
            setHeaders: { 
                'Accept': `application/json`
            }
        });
        return next.handle(request);
    }
}