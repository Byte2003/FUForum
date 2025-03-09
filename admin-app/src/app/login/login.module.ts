import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';
import { LoginComponent } from './login.component';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    NgxSpinner
  ],
  providers: [NgxSpinnerService],
})
export class LoginModule { }
