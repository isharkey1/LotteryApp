using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Enums;

namespace Lottery.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public bool HasWon { get; set; }
        public PrizeTier? PrizeTierWon { get; set; }
        public Guid BoughtById { get; set; }

        public Ticket(Guid id, Guid boughtById, decimal price)
        {
            Id = id;
            Price = price;
            HasWon = false;
            BoughtById = boughtById;
        }
    }
}
