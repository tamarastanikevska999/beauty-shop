import { Component } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../../service/account.service';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent  {
  errors: string[] | null = null;

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) {}

  complexPassword = "(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"

  registerForm = this.fb.group({
    username: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email], [this.validateEmailNotTaken()]],
    password: ['', [Validators.required, Validators.pattern(this.complexPassword)]],
  })  

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: error => this.errors = error.errors
    })
  }

  hasError(controlName: string, error: string) {
    const control = this.registerForm.get(controlName);
    return control && control.hasError(error);
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        take(1),
        switchMap(() => {
          return this.accountService.checkEmailExists(control.value).pipe(
            map(result => result ? {emailExists: true} : null),
            finalize(() => control.markAsTouched())
          )
        })
      )

    }
  }
}