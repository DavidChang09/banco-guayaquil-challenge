import { ApplicationConfig, ErrorHandler } from "@angular/core";
import { provideRouter } from "@angular/router";
import { provideAnimationsAsync } from "@angular/platform-browser/animations/async";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi  } from "@angular/common/http";
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from "@angular/material/form-field";
import { MAT_DATE_LOCALE } from "@angular/material/core";

import { routes } from "./app.routes";
import { GlobalErrorHander } from "./shared/global-error-handler";
import { ErrorInterceptor } from "./shared/error.interceptor";
import { KeycloakTokenInterceptor } from "./core/services/keycloak-token.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),

    // HTTP sin withInterceptorsFromDi()
    provideHttpClient(withInterceptorsFromDi()),

    { provide: MAT_DATE_LOCALE, useValue: "en-IN" },

    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { subscriptSizing: "dynamic" } },

    { provide: HTTP_INTERCEPTORS, useClass: KeycloakTokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    { provide: ErrorHandler, useClass: GlobalErrorHander },
  ],
};
