using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public bool State { get; set; }

        protected EntityBase()
        {
            State = true;
        }
    }
}
