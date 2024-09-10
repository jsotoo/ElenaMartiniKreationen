export class ProductDtoResponse {
  id: number;
  productId: number; // Alias para el ID del producto
  name: string;
  description: string;
  category: string;
  price: number;
  stock: number;
  imageUrl: string;
  quantity: number;
  cartItemId?: number; 
  cartId?: number | undefined;

  constructor(data: any) {
    this.id = data.id;
    this.productId = data.id; // Mapea id a productId
    this.name = data.name;
    this.description = data.description;
    this.price = data.price;
    this.stock = data.stock;
    this.category = data.category;
    this.imageUrl = data.imageUrl;
    this.quantity = data.quantity || 1;
    this.cartItemId = data.cartItemId;
    this.cartId = data.cartId;
  }
}
