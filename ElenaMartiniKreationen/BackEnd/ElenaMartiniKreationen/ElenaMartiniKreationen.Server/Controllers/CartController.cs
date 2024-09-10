using ElenaMartiniKreationen.Entities;
using ElenaMartiniKreationen.Repositories.Interfaces;
using ElenaMartiniKreationen.Server.Request;
using Microsoft.AspNetCore.Mvc;

namespace ElenaMartiniKreationen.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController: ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {

  
           return Ok(await _cartRepository.ListAsync());


        }

        [HttpPost]
        public async Task<IActionResult> Post(CartDtoRequest request)
        {
            var cart = new Cart()
            {
                Id = request.Id,
                UserId = request.UserId,
                CreationDate = request.CreationDate,


            };

            await _cartRepository.AddAsync(cart);

            return Ok(new { id = cart.Id });
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cartRepository.DeleteAsync(id);
            return Ok();
        }



    }
}
