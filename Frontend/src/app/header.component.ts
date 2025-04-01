import { ChangeDetectionStrategy, Component } from "@angular/core";
import { MatToolbarModule } from "@angular/material/toolbar";
// import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { RouterModule } from "@angular/router";
import { KeycloakService } from "./core/services/keycloak.service"; // Ajusta el path según tu estructura

@Component({
  selector: "app-header",
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule, RouterModule],
  template: `
    <p>
      <mat-toolbar color="primary">
        <span style="margin-right: 10px;">Wharehouse Management System</span>
        <button mat-button routerLink="/categories" routerLinkActive="active">
          Category
        </button>
        <button mat-button routerLink="/products" routerLinkActive="active">
          Products
        </button>
        <button mat-button routerLink="/stock" routerLinkActive="active">
          Stock
        </button>
        <button mat-button routerLink="/purchases" routerLinkActive="active">
          Purchases
        </button>
        <span class="example-spacer"></span>

        <button mat-button (click)="logout()">Logout</button>
      </mat-toolbar>
    </p>
  `,
  styles: [
    `
      .example-spacer {
        flex: 1 1 auto;
      }

      .active {
        background: #FF00FF;
      }
    `,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {

  constructor(private keycloak: KeycloakService) {}

  logout() {
    console.log(`Cerrando Sesion SSO ...`);
    this.keycloak.logout();
  }
}
