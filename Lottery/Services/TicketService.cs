using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Lottery.Interfaces;
using Lottery.Models;

namespace Lottery.Services
{
    public class TicketService : ITicketService
    {
        private static readonly Random _random = new Random();
        private const int MinTicketsAllowed = 1;
        private const int MaxTicketsAllowed = 10;
        private const decimal ticketPrice = 1.00m;

        public void HandleNonCpuPlayerTicketTransaction(Player player)
        {
            int numberOfTickets;

            Console.WriteLine($"How many tickets would you like to buy, {player.Name}?");

            while (!int.TryParse(Console.ReadLine(), out numberOfTickets) || numberOfTickets < MinTicketsAllowed || numberOfTickets > MaxTicketsAllowed) 
            {
                Console.WriteLine($"Invalid input. The allowed ticket range is between {MinTicketsAllowed} and {MaxTicketsAllowed}. Please enter a number within this range.");
            }

            PurchaseTickets(player, numberOfTickets);
        }

        public void PurchaseTicketsForCpuPlayers(List<Player> players)
        {
            var cpuPlayers = players.Where(p => p.IsCPU).ToList();

            for (int i = 0; i < cpuPlayers.Count; i++) 
            {
                int numberOfTickets = _random.Next(MinTicketsAllowed, MaxTicketsAllowed + 1);
                PurchaseTickets(cpuPlayers[i], numberOfTickets);
            }
        }

        public int CheckForSufficientFunds(decimal playerBalance, int ticketsRequested) 
        {
            return Math.Min((int)(playerBalance / ticketPrice), ticketsRequested);
        }

        public void PurchaseTickets(Player player, int numberOfTicketsRequested)
        {
            int ticketsAllowedAfterBalanceCheck = CheckForSufficientFunds(player.Balance, numberOfTicketsRequested);

            for (int i = 0; i < ticketsAllowedAfterBalanceCheck; i++) 
            {
                player.Tickets.Add(new Ticket(Guid.NewGuid(), player.Id, ticketPrice));
            }

            player.Balance = player.Balance - (ticketsAllowedAfterBalanceCheck * ticketPrice);
        }

        public void DisplayTicketsPurchased(List<Player> players)
        {
            var cpuPlayersCount = players.Where(p => p.IsCPU).Count();
            Console.WriteLine();
            Console.WriteLine($"{cpuPlayersCount} other CPU players have also purcahsed tickets:");
            Console.WriteLine();

            foreach (Player player in players)
            {
                Console.WriteLine($"*{player.Name} has purchased {player.Tickets.Count} tickets");
            }

            Console.WriteLine();
        }
    }
}
