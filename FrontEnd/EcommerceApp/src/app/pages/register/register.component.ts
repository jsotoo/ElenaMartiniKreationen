import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '@services/auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';  

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;
  registrationFailed: boolean = false;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService,private toastr: ToastrService,private router: Router) {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: [''],
      address: [''],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { 'mismatch': true };
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe(
        response => {
          this.toastr.success('Registro exitoso', 'Exito');
          this.registerForm.reset();  // Vacía los campos del formulario
          this.router.navigate(['/login']);  // Redirige a la página de login
        },
        error => {
          this.toastr.error('Registro fallido', 'Error');
          this.registrationFailed = true;
          this.errorMessage = error.error.title;
          // Muestra mensajes de error específicos
          if (error.error.errors) {
            this.errorMessage = Object.values(error.error.errors).join(' ');
          }
        }
      );
    }
  }
}
