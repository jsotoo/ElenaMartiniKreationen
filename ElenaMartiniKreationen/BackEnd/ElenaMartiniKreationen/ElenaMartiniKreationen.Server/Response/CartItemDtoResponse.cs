namespace ElenaMartiniKreationen.Server.Response
{
    public class CartItemDtoResponse: BaseResponse
    {
        public int Id { get; set; }

        public string Product { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public int Quantity { get; set; } = default!;

    }
}
