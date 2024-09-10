using ElenaMartiniKreationen.Entities;
using ElenaMartiniKreationen.Repositories.Implementations;
using ElenaMartiniKreationen.Repositories.Interfaces;
using ElenaMartiniKreationen.Server.Extensions;
using ElenaMartiniKreationen.Server.Request;
using ElenaMartiniKreationen.Server.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElenaMartiniKreationen.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : ControllerBase
    {

        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartItemController(ICartItemRepository cartItemRepository,
                                  ICartRepository cartRepository,
                                  IProductRepository productRepository
                                  )
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
         
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {

            var cartItems = await _cartItemRepository.ListAsync();
            var carts = await _cartRepository.ListAsync();
            var products = await _productRepository.ListAsync();


            var cartItemDto = carts.GetCartItemsByCartIdConvertToDto(cartItems, products, id);
            return Ok(cartItemDto);


        }
        
        [HttpPost]
        public async Task<IActionResult> Post(CartItemDtoRequest request)
        {

            var cartItem = new CartItem()
            {
                Id = request.Id,
                CartId = request.CartId,
                ProductId = request.ProductId,
                Quantity  = request.Quantity,
               

            };

            await _cartItemRepository.AddAsync(cartItem);

            return Ok(new { id = cartItem.Id });
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, CartItemDtoRequest request)
        {
            try
            {
                var registro = await _cartItemRepository.FindAsync(id);

                if (registro is null)
                {
                    return NotFound();
                }


                registro.ProductId = request.ProductId;
                registro.CartId = request.CartId;
                registro.Quantity = request.Quantity;


                if (request.Quantity < 0)
                {
                    return BadRequest("La cantidad no puede ser negativa");
                }


                await _cartItemRepository.UpdateAsync();
               

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message} \n\nInner Exception: {ex.InnerException?.Message}");
            }

        }




        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cartItemRepository.DeleteAsync(id);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetCartItemByProductId([FromQuery] int cartId, [FromQuery] int productId)
        {
            var cartItem = await _cartItemRepository.ListAsync(ci => ci.CartId == cartId && ci.ProductId == productId);

            if (cartItem == null)
            {
                return NotFound(); // Si no existe, retornar un 404
            }

            return Ok(cartItem); // Retornar el objeto encontrado
        }




    }
}
