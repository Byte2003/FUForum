import { NgModule } from '@angular/core';
import { CategoriesComponent } from './categories/categories.component';
import { CommentsComponent } from './comments/comments.component';
import { KnowledgeBasesComponent } from './knowledge-bases/knowledge-bases.component';
import { ReportsComponent } from './reports/reports.component';
import { ContentsRoutingModule } from './contents-routing.module';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PanelModule } from 'primeng/panel';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { InputText, InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { KeyFilterModule } from 'primeng/keyfilter';
import { TreeTableModule } from 'primeng/treetable';
import { DropdownModule } from 'primeng/dropdown';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { Textarea } from 'primeng/inputtextarea';
import { ChipModule } from 'primeng/chip';
import { FileUploadModule } from 'primeng/fileupload';
import { EditorModule } from 'primeng/editor';
import { NotificationService } from '../../shared/services/notification.service';
import { KnowledgeBasesDetailComponent } from './knowledge-bases-detail/knowledge-bases-detail.component';
import { ValidationMessageModule } from '../../shared/validation-message/validation-message.module';
import { CategoriesDetailComponent} from './categories/categories-detail/categories-detail.component';

@NgModule({
  declarations: [CategoriesComponent, 
    CategoriesDetailComponent,
    CommentsComponent, 
    KnowledgeBasesComponent, 
    ReportsComponent,
    CommentsComponent,
    ReportsComponent,
    KnowledgeBasesComponent,
    KnowledgeBasesDetailComponent,
  ],
  imports: [
    CommonModule,
    ContentsRoutingModule,
    PanelModule,
    ButtonModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    FormsModule,
    InputTextModule,
    ReactiveFormsModule,
    ProgressSpinnerModule,
    ValidationMessageModule,
    KeyFilterModule,
    CalendarModule,
    CheckboxModule,
    TreeTableModule,
    DropdownModule,
    Textarea,
    ChipModule,
    FileUploadModule,
    EditorModule,
    //SharedDirectivesModule,
    ModalModule.forRoot()
  ],
  providers: [DatePipe, NotificationService, BsModalService]
})
export class ContentsModule { }
