import Keycloak from 'keycloak-js';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class KeycloakService {
  private keycloak: Keycloak;
  private ready = false;

  constructor() {
    this.keycloak = new Keycloak({
      url: 'http://localhost:8080',
      realm: 'bco_guayaquil_realm',
      clientId: 'angular-client',
    });
  }

  async init(): Promise<void> {
    const authenticated = await this.keycloak.init({
      onLoad: 'login-required',
      checkLoginIframe: false
    });
    this.ready = authenticated;
  }

isReady(): boolean {
  return this.ready && !!this.keycloak.token;
}

getToken(): string | undefined {
  return this.isReady() ? this.keycloak.token : undefined;
}


logout(redirectUri?: string): void {
  this.keycloak.logout({
      redirectUri: redirectUri || window.location.origin
  });
}

  getUsername(): string | undefined {
    return this.keycloak.tokenParsed?.preferred_username;
  }
}
