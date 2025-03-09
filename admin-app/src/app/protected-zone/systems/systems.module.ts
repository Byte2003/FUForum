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
import { InputTextModule} from 'primeng/inputtext';
import { RolesAssignComponent } from './users/roles-assign/roles-assign.component';
import { UsersDetailComponent } from './users/users-detail/users-detail.component';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { KeyFilterModule } from 'primeng/keyfilter';
import { TreeTableModule } from 'primeng/treetable';
import { DropdownModule } from 'primeng/dropdown';
import { DatePipe } from '@angular/common';
import { FunctionsDetailComponent } from './functions/functions-detail/functions-detail.component';
import { CommandsAssignComponent } from './functions/commands-assign/commands-assign.component';

@NgModule({
  declarations: [FunctionsComponent, 
    UsersComponent, 
    PermissionsComponent, 
    RolesComponent, 
    RolesDetailComponent, 
    RolesAssignComponent, 
    UsersDetailComponent,
    FunctionsDetailComponent,
    CommandsAssignComponent],
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
    InputTextModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
    ValidationMessageModule,
    CalendarModule,
    CheckboxModule,
    KeyFilterModule,
    TreeTableModule,
    DropdownModule
  ],
  providers: [
    NotificationService,
    BsModalService,
    DatePipe
  ]
})
export class SystemsModule { }
