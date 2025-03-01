import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NgScrollbar } from 'ngx-scrollbar';
import {AuthService} from '../../shared/services/auth.service';
import { IconDirective } from '@coreui/icons-angular';
import {
  ContainerComponent,
  ShadowOnScrollDirective,
  SidebarBrandComponent,
  SidebarComponent,
  SidebarFooterComponent,
  SidebarHeaderComponent,
  SidebarNavComponent,
  SidebarToggleDirective,
  SidebarTogglerDirective
} from '@coreui/angular';

import { DefaultFooterComponent, DefaultHeaderComponent } from './';
import { navItems } from './_nav';
import { UserService } from '../../shared/services/user.service';
import { Function } from '../../shared/models';

function isOverflown(element: HTMLElement) {
  return (
    element.scrollHeight > element.clientHeight ||
    element.scrollWidth > element.clientWidth
  );
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
  imports: [
    SidebarComponent,
    SidebarHeaderComponent,
    SidebarBrandComponent,
    SidebarNavComponent,
    SidebarFooterComponent,
    SidebarToggleDirective,
    SidebarTogglerDirective,
    ContainerComponent,
    DefaultFooterComponent,
    DefaultHeaderComponent,
    IconDirective,
    NgScrollbar,
    RouterOutlet,
    RouterLink,
    ShadowOnScrollDirective
  ]
})
export class DefaultLayoutComponent {
  public navItems = [...navItems];
  public functions : Function[] = [];

  constructor(private userService: UserService, private authService: AuthService) {
    this.loadMenu();
  }

  loadMenu() {
    //const userId = '7b0f03fd-d76e-433a-9a00-54361e89c937'; // Hardcode, SSO will be implemented later
    //const profile = this.authService.getProfile;
    //console.log("Profile" + profile);

    // this.userService.getMenuByUser(userId).subscribe((response: Function[]) => {
    //   this.functions = this.buildHierarchy(response);
    //   console.log(this.functions);
    // });
  }

  buildHierarchy(items: Function[], parentId: string | null = null): Function[] {
    return items
      .filter(item => item.parentId === parentId)
      .map(item => ({
        ...item,
        children: this.buildHierarchy(items, item.id)
      }));
  }

}
