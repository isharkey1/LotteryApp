using Lottery.Interfaces;
using Lottery.Models;
using Lottery.Services;

namespace Lottery
{
    internal class Program
    {
        static void Main()
        {
            Casino casino = new Casino();

            INotificationService notificationService = new NotificationService(casino);
            IPlayerService playerService = new PlayerService();
            IPrizeService prizeService = new PrizeService(casino);
            ITicketService ticketService = new TicketService();
            IGameRunnerService gameRunnerService = new GameRunnerService(
                notificationService,
                playerService,
                prizeService,
                ticketService
            );

            gameRunnerService.RunGame();

            Console.WriteLine("Press any key to quit game");
            Console.ReadKey();
        }
    }
}

