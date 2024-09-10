// cart-dropdown.component.ts
import { Component, OnInit } from '@angular/core';
import { CartService } from '@services/cart/cart.service'; 
import { ProductDtoResponse } from '@services/product/response/product-dto-response'; 
import { ToastrService } from 'ngx-toastr'; // Importar ToastrService
@Component({
  selector: 'app-cart-dropdown',
  templateUrl: './cart-dropdown.component.html',
  styleUrls: ['./cart-dropdown.component.css']
})
export class CartDropdownComponent implements OnInit {
  cartItems: ProductDtoResponse[] = [];
  cartItemsCount: number = 0;
  
  constructor(private cartService: CartService,private toastr: ToastrService) {}

  ngOnInit(): void {
    this.cartService.cartItems$.subscribe(items => {
      console.log('Items recibidos en ngOnInit:', items);
      this.cartItems = items;
      this.updateCartCount();
    });
  }

  updateCartCount(): void {
    console.log('Calculando la cantidad total de artículos en el carrito...');
    this.cartItemsCount = this.cartItems.reduce((total, item) => total + (item.quantity || 0), 0);
    console.log('Cantidad total de artículos:', this.cartItemsCount);
  }

  removeFromCart(item: ProductDtoResponse): void {
    this.cartService.removeFromCart(item);
    this.toastr.info('Producto eliminado del carrito.');
  }

  clearCart(): void {
    this.cartService.clearCart();
    this.toastr.success('Carrito limpiado.');
  }

  goToCheckout(): void {
    // Lógica para navegar a la página de checkout
  }
}
