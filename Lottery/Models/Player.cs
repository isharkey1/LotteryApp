using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Ticket> Tickets { get; set; }
        public bool IsCPU { get; set; }
        public decimal Balance { get; set; }

        public Player(Guid id, string name, bool isCPU)
        {
            Id = id;
            Name = name;
            Tickets = new List<Ticket>();
            IsCPU = isCPU;
            Balance = 10.00m;
        }

    }
}
