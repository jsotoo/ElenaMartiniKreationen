import { Component,OnInit } from '@angular/core';
import { ProductService } from '../../services/product/product.service';
import { ProductDtoResponse } from '@services/product/response/product-dto-response';
import { SharedService } from '../../services/shared.service';
import { CartService } from '@services/cart/cart.service';
import { AuthService } from '@services/auth/auth.service';
import { Router } from '@angular/router';
declare var bootstrap: any; 

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  inputValue: string = '';
  products: ProductDtoResponse[] = [];
  filteredProducts: ProductDtoResponse[] = [];
  showCartDropdown = false;
  cartItems: ProductDtoResponse[] = []; // Array to hold cart items
  cartItemsCount: number = 0;
  isDropdownOpen = false; 
  fullName: string | null = null;
  isLoggedIn: boolean = false;
  userName: string = '';
  constructor(private sharedService: SharedService, private cartService: CartService, private authService: AuthService, private router: Router) {}

  getProductsByFilter(): void {
    this.sharedService.updateFilter(this.inputValue);
  }

  ngOnInit(): void {
    this.cartService.cartItems$.subscribe(items => {
      this.cartItems = items; // Update cart items from the service
      this.cartItemsCount = items.length; // Update cart count

      this.authService.user$.subscribe(user => {
        this.isLoggedIn = !!user; // Verifica si el usuario está autenticado
        if (user) {
          this.userName = user.fullName; // Asigna el nombre del usuario
        }
      });

    });

    const user = this.authService.getUser();
    this.fullName = user ? user.fullName : null;
  }

  removeFromCart(product: ProductDtoResponse) {
    this.cartService.removeFromCart(product);
  }

  clearCart() {
    this.cartService.clearCart();
  }

  goToCheckout() {
    // Lógica para ir al checkout
  }

  logout() {
    this.authService.logout();
   // this.authService.updateUser(null);
    this.router.navigate(['/login']);
  }

  toggleCartDropdown() {
    this.showCartDropdown = !this.showCartDropdown;
  }

 


}

  

