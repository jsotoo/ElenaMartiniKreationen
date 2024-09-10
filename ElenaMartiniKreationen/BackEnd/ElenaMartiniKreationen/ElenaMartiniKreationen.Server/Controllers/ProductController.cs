using ElenaMartiniKreationen.Entities;
using ElenaMartiniKreationen.Repositories.Interfaces;
using ElenaMartiniKreationen.Server.Response;
using Microsoft.AspNetCore.Mvc;

namespace ElenaMartiniKreationen.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string? filter)
        {
            var lista = await _productRepository.ListAsync(
            predicate: p => p.State && p.Name.Contains(filter ?? string.Empty),
              selector: x => new ProductDtoResponse
              {
                  Id = x.Id,
                  Name = x.Name,
                  Description = x.Description,
                  Price = x.Price,
                  Stock = x.Stock,
                  ImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{x.ImageUrl}"

              }, relations: "");

            return Ok(lista);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {

            var product = await _productRepository.FindAsync(id);

            if (product == null)
            {
                return NotFound(); 
            }

            var productDto = new ProductDtoResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{product.ImageUrl}"
            };


            return Ok(productDto);
        }
    }
}
