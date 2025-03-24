using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Dtos;
using Lottery.Interfaces;
using Lottery.Models;
using Lottery.Enums;

namespace Lottery.Services
{
    public class NotificationService : INotificationService
    {
        private readonly Casino _casino;

        public NotificationService(Casino casino)
        {
            _casino = casino;
        }

        public void ShowWelcomeMessage(Player player, decimal ticketPrice)
        {
            Console.WriteLine($"Welcome to the {_casino.Name} Lottery, {player.Name}!");
            Console.WriteLine();
            Console.WriteLine($"* Your digital balance: ${player.Balance}");
            Console.WriteLine($"* Ticket Price: ${ticketPrice} each");
            Console.WriteLine();
        }

        public void ShowWinnersMessage(List<WinningTicketDto> winningTickets)
        {
            StringBuilder winnersMessage = new StringBuilder();
            winnersMessage.AppendLine("Ticket Draw Results:");
            winnersMessage.AppendLine();

            var grandPrizeWinningTicket = winningTickets.FirstOrDefault(wt => wt.PrizeTier == PrizeTier.GrandPrize);

            if (grandPrizeWinningTicket != null)
            {
                winnersMessage.AppendLine($"* Grand Prize: {grandPrizeWinningTicket.PlayerName} wins ${grandPrizeWinningTicket.PrizeAmount:F2}!");
            }

            var secondTierWinningTickets = winningTickets.Where(wt => wt.PrizeTier == PrizeTier.SecondTier).ToList();

            if (secondTierWinningTickets.Any())
            {
                decimal prizeAmount = secondTierWinningTickets[0].PrizeAmount;

                winnersMessage.Append("* Second Tier: Players ");
                var formattedNames = secondTierWinningTickets
                    .Select(p => p.PlayerName.Replace("Player ", ""))
                    .ToList();

                winnersMessage.AppendLine($"{string.Join(", ", formattedNames)} win ${prizeAmount:F2}");
            }

            var thirdTierWinningTickets = winningTickets.Where(wt => wt.PrizeTier == PrizeTier.ThirdTier).ToList();

            if (thirdTierWinningTickets.Any())
            {
                decimal prizeAmount = thirdTierWinningTickets[0].PrizeAmount;

                winnersMessage.Append("* Third Tier: Players ");
                var formattedNames = thirdTierWinningTickets
                    .Select(p => p.PlayerName.Replace("Player ", ""))
                    .ToList();

                winnersMessage.AppendLine($"{string.Join(", ", formattedNames)} win ${prizeAmount:F2}");
            }

            winnersMessage.AppendLine();

            Console.WriteLine(winnersMessage.ToString());
        }

        public void ShowGameConclusionMessage()
        {
            Console.WriteLine("Congratulations to the winners!");
            Console.WriteLine();
            Console.WriteLine($"House Revenue: ${_casino.Revenue:F2}");
        }
    }
}
