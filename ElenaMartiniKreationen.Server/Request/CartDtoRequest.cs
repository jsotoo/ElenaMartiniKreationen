using ElenaMartiniKreationen.Entities;

namespace ElenaMartiniKreationen.Server.Request
{
    public class CartDtoRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
