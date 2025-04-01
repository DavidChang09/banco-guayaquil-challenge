// src/app/core/interceptors/keycloak-token.interceptor.ts
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { KeycloakService } from '../services/keycloak.service';

@Injectable({
    providedIn: 'root' 
})
export class KeycloakTokenInterceptor implements HttpInterceptor {

    constructor(private keycloak: KeycloakService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = this.keycloak.getToken();
        console.log(`Imprimiendo Token ${token}`);

        // Si no hay token aún, sigue sin modificar
        if (!token) {
            return next.handle(req);
        }

        // Si hay token, clona la request con el Authorization header
        const cloned = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });

        return next.handle(cloned);
    }
}
