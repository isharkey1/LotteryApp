using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Enums;

namespace Lottery.Dtos
{
    public class WinningTicketDto
    {
        public Guid TicketId { get; set; }
        public PrizeTier PrizeTier { get; set; }
        public decimal PrizeAmount { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        
        public WinningTicketDto(Guid ticketId, PrizeTier prizeTier, decimal prizeAmount, Guid playerId, string playerName)
        {
            TicketId = ticketId;
            PrizeTier = prizeTier;
            PrizeAmount = prizeAmount;
            PlayerId = playerId;
            PlayerName = playerName;
        }
    }

    
}
