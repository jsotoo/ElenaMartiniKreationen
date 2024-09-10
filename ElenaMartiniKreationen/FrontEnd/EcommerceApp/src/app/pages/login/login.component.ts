// login.component.ts
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '@services/auth/auth.service';
import { ToastrService } from 'ngx-toastr'; // Importar ToastrService
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder,
    private toastr: ToastrService
  )
   {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void { }

 // login.component.ts
 onLogin() {
  if (this.loginForm.valid) {
    const { username, password } = this.loginForm.value;

    this.authService.login({ User: username, Password: password }).subscribe({
      next: (data) => {
        if (data && data.token) {
          // Guarda el token y los datos del usuario en el almacenamiento local
          localStorage.setItem('authToken', data.token);
          this.toastr.success(`Bienvenido ${data.fullName}!`, 'Inicio de Sesión Exitoso');
          this.router.navigate(['/home']);

          this.router.navigate(['/home']);
        }
      },
      error: (err) => {
        this.toastr.error('Credenciales incorrectas o usuario no encontrado.', 'Error');
      }
    });
  } else {
    this.toastr.warning('Por favor, complete todos los campos.', 'Formulario Incompleto');
  }
}

}
