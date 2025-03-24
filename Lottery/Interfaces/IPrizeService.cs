using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Dtos;
using Lottery.Enums;
using Lottery.Models;

namespace Lottery.Interfaces
{
    public interface IPrizeService
    {
        List<WinningTicketDto> RunPrizeDraws(List<Player> players);
        void RunGrandPrizeDraw(List<Player> players, List<Ticket> remainingTickets, List<WinningTicketDto> winningTickets, decimal prizeTotal, PrizeTier prizeTier);
        void RunMultipleWinnersDraw(List<Player> players, List<Ticket> remainingTickets, List<WinningTicketDto> winningTickets, decimal prizeTotal, int numberOfWinningPlayers, PrizeTier prizeTier);
        List<Ticket> GetRemainingTickets(List<Player> players);
    }
}
