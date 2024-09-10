using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class Category:EntityBase
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

    }
}
