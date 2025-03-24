using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Models
{
    public class Casino
    {
        public decimal Revenue { get; set; }
        public string Name { get; set; } 

        public Casino()
        {
            Revenue = 0.00m;
            Name = "Bede";
        }
    }
}
