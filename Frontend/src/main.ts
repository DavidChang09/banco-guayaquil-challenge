// src/main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';
import { provideZoneChangeDetection, inject } from '@angular/core';
import { KeycloakService } from './app/core/services/keycloak.service';

bootstrapApplication(AppComponent, appConfig)
  .then(async appRef => {
    const keycloak = appRef.injector.get(KeycloakService);
    await keycloak.init();
  })
  .catch(err => console.error(err));
