import { Injectable } from '@angular/core';
import { ProductDtoResponse } from '@services/product/response/product-dto-response'; 
import { BehaviorSubject, Observable, throwError, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { catchError, tap, map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems = new BehaviorSubject<ProductDtoResponse[]>([]);
  cartItems$ = this.cartItems.asObservable();

  private apiCartUrl = 'https://localhost:7183/api/Cart';
  private apiCartItemUrl = 'https://localhost:7183/api/CartItem';

  private currentCartId: number | null = null;  // Guardar el ID del carrito

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  clearCart(): void {
    this.cartItems.next([]); // Limpia el carrito localmente
    this.currentCartId = null; // Resetea el ID del carrito actual
  }

  addToCart(product: ProductDtoResponse): void {
    if (this.currentCartId === null) {
      this.createCart().pipe(
        tap(cartId => {
          this.currentCartId = cartId;
          product.cartId = cartId;
        
        }),
        switchMap(cartId => this.getCartItemByProductId(cartId, product.id)),
        switchMap(response => this.handleCartItemResponse(response, product)),
      ).subscribe({
        next: response => this.handleCartItemSuccess(response, product),
        error: this.handleCartItemError
      });
    } else {
      product.cartId = this.currentCartId;
      this.getCartItemByProductId(this.currentCartId, product.id).pipe(
        switchMap(response => this.handleCartItemResponse(response, product)),
      ).subscribe({
        next: response => this.handleCartItemSuccess(response, product),
        error: this.handleCartItemError
      });
    }
  }

  private handleCartItemResponse(response: any[], product: ProductDtoResponse): Observable<any> {
    if (response && response.length > 0) {
      const existingItem = response[0];
      console.log("Producto ya existe en el carrito:", existingItem);
  
      existingItem.cartItemId = existingItem.id || existingItem.cartItemId;
      existingItem.cartId = this.currentCartId;
      existingItem.productId = product.id;
  
      if (!existingItem.cartItemId) {
        console.error("El ID del CartItem es necesario para actualizar el producto en el carrito.");
        this.toastr.error("Error al actualizar producto en el carrito", "Falta ID del CartItem");
        return throwError(() => new Error("El ID del CartItem es necesario."));
      }
  
      existingItem.quantity += 1;
      return this.updateCartItem(existingItem);
    }else {
      if (this.currentCartId !== null) {
        const cartItemRequest = { 
          cartId: this.currentCartId, 
          productId: product.id, 
          quantity: 1,
          // Incluye cualquier otro campo que tu API pueda requerir
        };
        console.log("Producto no existe en el carrito, creando nuevo", cartItemRequest);
        return this.createCartItem(cartItemRequest);
      } else {
        return throwError(() => new Error('El ID del carrito es null'));
      }
    }
  }

  private handleCartItemSuccess(response: any, product: ProductDtoResponse): void {
    console.log('Respuesta recibida en handleCartItemSuccess:', response);
    if (response) {
      product.cartItemId = response.id || product.cartItemId;
      this.updateCartState(product);
    } else {
      console.error('Response no válido:', response);
      this.toastr.error('Error al procesar la respuesta del servidor', 'Error');
    }
  }
  private handleCartItemError(error: any): void {
    console.error('Error al añadir producto al carrito:', error);
    this.toastr.error('Error al añadir producto al carrito', 'Error');
    }

    removeFromCart(product: ProductDtoResponse): void {
      const cartItemIndex = this.cartItems.value.findIndex(item => item.id === product.id);
    
      if (cartItemIndex > -1) {
        const existingItem = this.cartItems.value[cartItemIndex];
        existingItem.cartId = this.currentCartId ?? existingItem.cartId;
        existingItem.productId = product.id;
        if (existingItem.quantity > 1) {
          existingItem.quantity -= 1;
          console.log('Actualizando item existente:', existingItem);
    
          this.updateCartItem(existingItem).subscribe({
            next: (updatedItem) => {
              console.log('Item actualizado:', updatedItem);
              this.cartItems.next([...this.cartItems.value]);
            },
            error: (error) => console.error('Error al actualizar item:', error)
          });
        } else {
          if (existingItem.cartItemId !== undefined) {
            this.deleteCartItem(existingItem.cartItemId).subscribe({
              next: () => {
                this.cartItems.value.splice(cartItemIndex, 1);
                this.cartItems.next([...this.cartItems.value]);
              },
              error: (error) => console.error('Error al eliminar item:', error)
            });
          }
        }
      }
    }

    private updateCartState(product: ProductDtoResponse) {
      const currentCartItems = this.cartItems.value;
      const existingItem = currentCartItems.find(item => item.id === product.id);
    
      if (existingItem) {
        existingItem.quantity += 1;
        existingItem.cartId = this.currentCartId ?? -1; // Usar -1 como valor por defecto
      } else {
        product.quantity = 1;
        product.cartId = this.currentCartId ?? undefined; // Usar undefined si es null
        currentCartItems.push(product);
      }
    
      this.cartItems.next([...currentCartItems]);
    }

  private createCart(): Observable<number> {
    const cart = {
      userId: 5,
      creationDate: new Date()
    };

    return this.http.post<any>(this.apiCartUrl, cart).pipe(
      tap(() => this.toastr.success('Carrito creado exitosamente', 'Éxito')),
      map(response => response.id),
      catchError(error => {
        this.toastr.error('Error al crear el carrito', 'Error');
        throw error;
      })
    );
  }

  private createCartItem(cartItem: { cartId: number, productId: number, quantity: number }): Observable<any> {
    return this.http.post<any>(this.apiCartItemUrl, cartItem).pipe(
      tap(response => {
        console.log('Respuesta de createCartItem:', response);
        this.toastr.success('Producto añadido al carrito', 'Éxito');
      }),
      map(response => {
        if (!response || !response.id) {
          throw new Error('La respuesta del servidor no contiene un ID válido');
        }
        return {
          id: response.id,
          productId: response.productId,
          quantity: response.quantity
        };
      }),
      catchError(error => {
        this.toastr.error('Error al añadir producto al carrito', 'Error');
        return throwError(() => error);
      })
    );
  }

  private updateCartItem(product: ProductDtoResponse): Observable<any> {
    console.log('Datos recibidos en updateCartItem:', product);
  
    if (!product.cartItemId) {
      console.error('El ID del CartItem es necesario para actualizar el producto en el carrito.');
      this.toastr.error('No se puede actualizar el carrito porque falta el ID del CartItem.', 'Error');
      return throwError(() => new Error('El ID del CartItem es necesario.'));
    }
  
    if (!product.cartId || !product.productId) {
      console.error('cartId y productId son necesarios para actualizar el CartItem.');
      this.toastr.error('Faltan datos necesarios para actualizar el carrito.', 'Error');
      return throwError(() => new Error('cartId y productId son necesarios.'));
    }
  
    const cartItem = {
      id: product.cartItemId,
      cartId: product.cartId,
      productId: product.productId,
      quantity: product.quantity
    };
  
    console.log('Enviando al servidor:', cartItem);
  
    return this.http.put(`${this.apiCartItemUrl}/${product.cartItemId}`, cartItem).pipe(
      tap(response => {
        console.log('Respuesta de updateCartItem:', response);
        this.toastr.success('Carrito actualizado', 'Éxito');
      }),
      map(response => {
        if (!response) {
          console.log('La respuesta del servidor es nula, devolviendo el cartItem original');
          return cartItem;
        }
        return response;
      }),
      catchError(error => {
        console.error('Error al actualizar el carrito:', error);
        this.toastr.error('Error al actualizar el carrito', 'Error');
        return throwError(() => error);
      })
    );
  }

  private deleteCartItem(cartItemId: number): Observable<any> {
    return this.http.delete(`${this.apiCartItemUrl}/${cartItemId}`).pipe(
      tap(() => this.toastr.success('Producto eliminado del carrito', 'Éxito')),
      catchError(error => {
        this.toastr.error('Error al eliminar el producto del carrito', 'Error');
        return throwError(() => error);
      })
    );
  }

  private getCartItemByProductId(cartId: number, productId: number): Observable<any> {
    console.log(`Buscando CartItem con cartId=${cartId} y productId=${productId}`);
    
    const url = `${this.apiCartItemUrl}?cartId=${cartId}&productId=${productId}`;
    console.log(`URL para la consulta: ${url}`);

    return this.http.get<any>(url).pipe(
      tap(response => {
        console.log('Respuesta de getCartItemByProductId:', response);
      }),
      catchError(error => {
        console.error('Error al obtener el item del carrito:', error);
        this.toastr.error('Error al obtener el item del carrito', 'Error');
        return throwError(() => error);
      })
    );
  }
}