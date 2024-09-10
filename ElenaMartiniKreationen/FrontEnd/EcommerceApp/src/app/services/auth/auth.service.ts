import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7183/api/users'; // Reemplaza con la URL de tu API
  private userSubject = new BehaviorSubject<any>(null);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {

  }

  login(user: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Login`, user)
    .pipe(tap(response => {
      if (response && response.token && response.fullName) {
        // Almacena el token y el nombre del usuario en el BehaviorSubject
        this.userSubject.next({ token: response.token, fullName: response.fullName });
        console.log(response.fullName);

        // Almacena el token en localStorage (si lo necesitas)
        localStorage.setItem('authToken', response.token);
        localStorage.setItem('user', response.fullName);
      }
    }));
  }

  logout() {
    // Elimina el token y el usuario del almacenamiento local
    localStorage.removeItem('authToken');
    localStorage.removeItem('user');
    this.userSubject.next(null);
  }

  // Método para obtener el valor actual del usuario
  get currentUserValue() {
    return this.userSubject.value;
  }
  register(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Register`, userData);
  }
 // auth.service.ts
 updateUser(user: any) {
  this.userSubject.next(user);
  localStorage.setItem('user', JSON.stringify(user)); // Guarda el usuario en el localStorage
}

getUser() {
  return JSON.parse(localStorage.getItem('user')!); // Obtén el usuario del localStorage
}

  
}
