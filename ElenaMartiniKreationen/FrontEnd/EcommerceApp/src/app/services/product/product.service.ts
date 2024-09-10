import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductDtoResponse } from './response/product-dto-response'; 

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7183/api/product'; // Cambia esta URL por la de tu API  
  
  constructor(private http: HttpClient) { }
  
  getProducts(): Observable<ProductDtoResponse[]> {
    return this.http.get<ProductDtoResponse[]>(this.apiUrl);
  }

  getProductsByFilter(filter: string): Observable<ProductDtoResponse[]> {
    const url = `${this.apiUrl}?filter=${filter}`;
    return this.http.get<ProductDtoResponse[]>(url);
  }


  getProductsById(id: string): Observable<ProductDtoResponse> {
    return this.http.get<ProductDtoResponse>(`${this.apiUrl}/${id}`);
  }


}
