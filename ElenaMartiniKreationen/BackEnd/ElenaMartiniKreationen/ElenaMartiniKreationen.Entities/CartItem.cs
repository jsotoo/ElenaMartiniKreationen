﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class CartItem: EntityBase
    {
        //Foreign Key
        public int CartId { get; set; }
        public Cart Cart { get; set; } = default!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int Quantity { get; set; }

    }
}
