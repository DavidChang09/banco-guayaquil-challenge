import { ChangeDetectionStrategy, Component } from "@angular/core";

@Component({
  selector: "app-footer",
  standalone: true,
  imports: [],
  template: `
    Warehouse Management System
  `,
  styles: [
    `
      :host {
        padding: 10px 0px;
        text-align: center;
        font-size: 16px;
      }
    `,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FooterComponent {}
