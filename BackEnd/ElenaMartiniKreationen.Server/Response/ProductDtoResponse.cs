﻿namespace ElenaMartiniKreationen.Server.Response
{
    public class ProductDtoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal? Price { get; set; }
        public int Stock { get; set;}
        public string ImageUrl { get; set; } = default!;
    }
}
