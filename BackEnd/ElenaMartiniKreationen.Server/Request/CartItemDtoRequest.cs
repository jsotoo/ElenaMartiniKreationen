using ElenaMartiniKreationen.Entities;

namespace ElenaMartiniKreationen.Server.Request
{
    public class CartItemDtoRequest
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; } 
        public int Quantity { get; set; }
    }
}
