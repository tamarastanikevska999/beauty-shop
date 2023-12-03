import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatCardModule
  ]
})
export class AccountModule { }
