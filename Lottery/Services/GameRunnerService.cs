using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Interfaces;
using Lottery.Models;

namespace Lottery.Services
{
    public class GameRunnerService : IGameRunnerService
    {
        private decimal TicketPrice = 1.00m;

        private readonly INotificationService _notificationService;
        private readonly IPlayerService _playerService;
        private readonly IPrizeService _prizeService;
        private readonly ITicketService _ticketService;

        public GameRunnerService(
            INotificationService notificationService, 
            IPlayerService playerService, 
            IPrizeService prizeService, 
            ITicketService ticketService)
        {
            _notificationService = notificationService;
            _playerService = playerService;
            _prizeService = prizeService;
            _ticketService = ticketService;
        }

        public void RunGame()
        {
            var players = _playerService.SetupPlayers();
            var nonCpuPlayer = players[0];

            _notificationService.ShowWelcomeMessage(nonCpuPlayer, TicketPrice);

            _ticketService.HandleNonCpuPlayerTicketTransaction(nonCpuPlayer);
            _ticketService.PurchaseTicketsForCpuPlayers(players);
            _ticketService.DisplayTicketsPurchased(players);

            var winningTickets = _prizeService.RunPrizeDraws(players);

            _notificationService.ShowWinnersMessage(winningTickets);
            _notificationService.ShowGameConclusionMessage();
        }
    }
}
