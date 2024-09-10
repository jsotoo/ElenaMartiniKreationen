import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // Asegúrate de importar esto
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './pages/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { ProductsComponent } from './pages/products/products.component';
import { ProductDetailComponent } from './pages/product-detail/product-detail.component';
import { AppRoutingModule } from './app-routing.module';
import { CartDropdownComponent } from './components/cart-dropdown/cart-dropdown.component';
import { LoginComponent } from './pages/login/login.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { RegisterComponent } from './pages/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UserDropdownComponent } from './components/user-dropdown/user-dropdown.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; // Importa ReactiveFormsModule
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // Agregar esto
import { ToastrModule } from 'ngx-toastr'; // Agregar esto
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    ProductsComponent,
    ProductDetailComponent,
    CartDropdownComponent,
    LoginComponent,
    RegisterComponent,
    UserDropdownComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule, // Agregar esto
    ToastrModule.forRoot() // Agregar esto
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },],
  bootstrap: [AppComponent]
})
export class AppModule { }
