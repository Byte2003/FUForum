import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SystemsRoutingModule } from './systems-routing.module';
import { FunctionsComponent } from './functions/functions.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { PanelModule } from 'primeng/panel';
import { ButtonModule} from 'primeng/button';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import {BlockUI} from 'primeng/blockui';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import { RolesDetailComponent } from './roles/roles-detail/roles-detail.component';
import { NotificationService } from '../../shared/services';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ValidationMessageModule } from '../../shared/validation-message/validation-message.module';
@NgModule({
  declarations: [FunctionsComponent, UsersComponent, PermissionsComponent, RolesComponent, RolesDetailComponent],
  imports: [
    CommonModule,
    SystemsRoutingModule,
    PanelModule,
    ButtonModule,
    FormsModule,
    TableModule,
    PaginatorModule,
    BlockUI,
    ProgressSpinnerModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    ValidationMessageModule
  ],
  providers: [
    NotificationService,
    BsModalService
  ]
})
export class SystemsModule { }
