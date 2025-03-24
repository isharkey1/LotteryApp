using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Dtos;
using Lottery.Enums;
using Lottery.Interfaces;
using Lottery.Models;

namespace Lottery.Services
{
    public class PrizeService : IPrizeService
    {
        private readonly Casino _casino;
        private static readonly Random _random = new Random();
        private decimal GrandPrizeRevenueShare = 0.5m;
        private decimal SecondTierRevenueShare = 0.3m;
        private decimal ThirdTierRevenueShare = 0.1m;
        private decimal SecondTierTicketSplit = 0.1m;
        private decimal ThirdTierTicketSplit = 0.2m;

        public PrizeService(Casino casino)
        {
            _casino = casino;
        }


        public List<WinningTicketDto> RunPrizeDraws(List<Player> players)
        {
            decimal totalTicketRevenue = players.Sum(p => p.Tickets.Sum(t => t.Price));

            decimal grandPrizeTotal = Math.Round(totalTicketRevenue * GrandPrizeRevenueShare, 2, MidpointRounding.ToZero);
            decimal secondTierPrizeTotal = Math.Round(totalTicketRevenue * SecondTierRevenueShare, 2, MidpointRounding.ToZero);
            decimal thirdTierPrizeTotal = Math.Round(totalTicketRevenue * ThirdTierRevenueShare, 2, MidpointRounding.ToZero);

            List<WinningTicketDto> winningTickets = new List<WinningTicketDto>();
            List<Ticket> remainingTickets = GetRemainingTickets(players);

            if (remainingTickets.Count > 0)
            {
                RunGrandPrizeDraw(players, remainingTickets, winningTickets, grandPrizeTotal, PrizeTier.GrandPrize);
            }

            if (remainingTickets.Count > 0) 
            {
                int numberOfWinners = (int)Math.Round(remainingTickets.Count * SecondTierTicketSplit);
                RunMultipleWinnersDraw(players, remainingTickets, winningTickets, secondTierPrizeTotal, numberOfWinners, PrizeTier.SecondTier);
            }

            if (remainingTickets.Count > 0)
            {
                int numberOfWinners = (int)Math.Round(remainingTickets.Count * ThirdTierTicketSplit);
                RunMultipleWinnersDraw(players, remainingTickets, winningTickets, thirdTierPrizeTotal, numberOfWinners, PrizeTier.ThirdTier);
            }

            decimal totalPrizeAmount = grandPrizeTotal + secondTierPrizeTotal + thirdTierPrizeTotal;
            decimal leftoverRevenue = totalTicketRevenue - totalPrizeAmount;

            _casino.Revenue += leftoverRevenue;

            return winningTickets;
        }

        public void RunGrandPrizeDraw(List<Player> players, List<Ticket> remainingTickets, List<WinningTicketDto> winningTickets, decimal prizeTotal, PrizeTier prizeTier)
        {
            decimal roundedPrizeTotal = Math.Round(prizeTotal, 2, MidpointRounding.ToZero); ;
            decimal leftoverAmount = prizeTotal - roundedPrizeTotal;

            AwardPrize(players, remainingTickets, winningTickets, roundedPrizeTotal, 1, prizeTier);

            _casino.Revenue += leftoverAmount;
        }

        public void RunMultipleWinnersDraw(List<Player> players, List<Ticket> remainingTickets, List<WinningTicketDto> winningTickets, decimal prizeTotal, int numberOfWinningPlayers, PrizeTier prizeTier)
        {
            if (numberOfWinningPlayers > 0)
            {
                decimal perPlayerPrizeAmount = Math.Round(prizeTotal / numberOfWinningPlayers, 2, MidpointRounding.ToZero);
                decimal totalRoundedPrizeAmount = perPlayerPrizeAmount * numberOfWinningPlayers;

                decimal leftoverAmount = prizeTotal - totalRoundedPrizeAmount;

                AwardPrize(players, remainingTickets, winningTickets, perPlayerPrizeAmount, numberOfWinningPlayers, prizeTier);

                _casino.Revenue += leftoverAmount;
            }
        }

        private static void AwardPrize(List<Player> players, List<Ticket> remainingTickets, List<WinningTicketDto> winningTickets, decimal prizeAmount, int numberOfWinners, PrizeTier prizeTier)
        {
            for (int i = 0; i < numberOfWinners && remainingTickets.Count > 0; i++)
            {
                var winningTicket = remainingTickets[_random.Next(remainingTickets.Count)];
                var winningPlayer = players.First(player => player.Id == winningTicket.BoughtById);

                winningPlayer.Balance += prizeAmount;
                winningTickets.Add(new WinningTicketDto(winningTicket.Id, prizeTier, prizeAmount, winningPlayer.Id, winningPlayer.Name));
                winningTicket.HasWon = true;
                remainingTickets.Remove(winningTicket);
            }
        }

        public List<Ticket> GetRemainingTickets(List<Player> players)
        {
            return [.. players.SelectMany(player => player.Tickets).Where(ticket => !ticket.HasWon)];
        }
    }
}
