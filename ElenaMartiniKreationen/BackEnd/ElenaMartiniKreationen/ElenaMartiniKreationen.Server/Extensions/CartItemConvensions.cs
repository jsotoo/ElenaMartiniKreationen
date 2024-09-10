using ElenaMartiniKreationen.Entities;
using ElenaMartiniKreationen.Server.Response;
using Microsoft.AspNetCore.Http.Metadata;
using System.Runtime.CompilerServices;

namespace ElenaMartiniKreationen.Server.Extensions
{
    public static class CartItemConvensions
    {
        public static IEnumerable<CartItemDtoResponse> GetCartItemsByCartIdConvertToDto(this IEnumerable<Cart> Carts,
                                                                    IEnumerable<CartItem> cartItems,
                                                                    IEnumerable<Product> products, int id)
        {
            return (from cart in Carts
                    join cartitem in cartItems on cart.Id equals cartitem.CartId
                    join product in products on cartitem.ProductId equals product.Id
                    where cart.Id == id
                    select new CartItemDtoResponse
                    {
                        Id = cartitem.Id,
                        Product = product.Name,
                        ImageUrl = product.ImageUrl,
                        Quantity = cartitem.Quantity

                    }).ToList();


        }
    }
}

