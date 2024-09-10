import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from '@services/product/product.service';
import { ProductDtoResponse } from '@services/product/response/product-dto-response';
import { SharedService } from '../../services/shared.service';
import { Router } from '@angular/router';
import { CartService } from '@services/cart/cart.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit  {
  @Input()products: ProductDtoResponse[] = [];
  filteredProducts: ProductDtoResponse[] = [];
  selectedCategory: string = 'all';

  constructor(private router: Router, private productService: ProductService, private sharedService: SharedService, private cartService: CartService) {}

  onProductSelected(product: ProductDtoResponse): void {
    this.router.navigate(['/product', product.productId]);
  }



  ngOnInit(): void {
    // Inicializar productos sin filtro
    this.productService.getProducts().subscribe({
      next: (data: ProductDtoResponse[]) => {
        this.products = data;
        this.filteredProducts = data; // Muestra todos los productos al inicio
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      }
    });

    // Escuchar cambios de filtro
    this.sharedService.currentFilter$.subscribe(filter => {
      if (filter) {
        this.productService.getProductsByFilter(filter).subscribe({
          next: (data: ProductDtoResponse[]) => {
            this.filteredProducts = data;
          },
          error: (error) => {
            console.error('Error fetching filtered products:', error);
          }
        });
      } else {
        this.filteredProducts = this.products; // Si no hay filtro, muestra todos los productos
      }
    });
}

addToCart(product: ProductDtoResponse): void {
  this.cartService.addToCart(product);
}

}
