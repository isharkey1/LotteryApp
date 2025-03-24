using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Models;

namespace Lottery.Interfaces
{
    public interface ITicketService
    {
        void HandleNonCpuPlayerTicketTransaction(Player player);
        void PurchaseTicketsForCpuPlayers(List<Player> players);
        int CheckForSufficientFunds(decimal playerBalance, int ticketsRequested);
        void PurchaseTickets(Player player, int noOfTickets);
        void DisplayTicketsPurchased(List<Player> players);
    }
}
