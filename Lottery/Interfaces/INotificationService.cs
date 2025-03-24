using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Dtos;
using Lottery.Models;

namespace Lottery.Interfaces
{
    public interface INotificationService
    {
        void ShowWelcomeMessage(Player player, decimal ticketPrice);
        void ShowWinnersMessage(List<WinningTicketDto> winningTickets);
        void ShowGameConclusionMessage();
    }
}
