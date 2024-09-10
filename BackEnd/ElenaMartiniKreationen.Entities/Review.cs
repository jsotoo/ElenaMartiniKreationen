using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class Review: EntityBase
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = default!;
        public DateTime Date { get; set; }

        // Foreign Key
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}
