using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Models;
using Lottery.Services;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace Lottery.Tests.Services
{
    public class TicketServiceTest
    {
        private readonly TicketService _ticketService;

        public TicketServiceTest()
        {
            _ticketService = new TicketService();
        }

        [Fact]
        public void CheckForSufficientFunds_ShouldReturnCorrectTicketCount()
        {
            // Arrange
            decimal balance = 5.00m;
            int requestedTickets = 10;

            // Act
            int allowedTickets = _ticketService.CheckForSufficientFunds(balance, requestedTickets);

            // Assert
            Assert.Equal(5, allowedTickets); 
        }

        [Fact]
        public void PurchaseTickets_ShouldDeductBalanceAndAddTickets()
        {
            // Arrange
            var player = new Player(Guid.NewGuid(), "Test Player", false) { Balance = 5.00m };
            int ticketsRequested = 3;

            // Act
            _ticketService.PurchaseTickets(player, ticketsRequested);

            // Assert
            Assert.Equal(3, player.Tickets.Count); 
            Assert.Equal(2.00m, player.Balance);   
        }

        [Fact]
        public void PurchaseTicketsForCpuPlayers_ShouldBuyTicketsWithinRange()
        {
            // Arrange
            var cpuPlayers = new List<Player>
            {
                new Player(Guid.NewGuid(), "Player 2", true) { Balance = 10.00m },
                new Player(Guid.NewGuid(), "Player 3", true) { Balance = 10.00m },
                new Player(Guid.NewGuid(), "Player 4", true) { Balance = 10.00m }
            };

            // Act
            _ticketService.PurchaseTicketsForCpuPlayers(cpuPlayers);

            // Assert
            foreach (var cpu in cpuPlayers)
            {
                Assert.InRange(cpu.Tickets.Count, 1, 10);
            }
        }

        [Fact]
        public void DisplayTicketsPurchased_ShouldPrintCorrectOutput()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player(Guid.NewGuid(), "Player 1", false) { Tickets = new List<Ticket> { new Ticket(Guid.NewGuid(), Guid.NewGuid(), 1.00m) } },
                new Player(Guid.NewGuid(), "Player 2", true) { Tickets = new List<Ticket> { new Ticket(Guid.NewGuid(), Guid.NewGuid(), 1.00m), new Ticket(Guid.NewGuid(), Guid.NewGuid(), 1.00m) } },
                new Player(Guid.NewGuid(), "Player 3", true) { Tickets = new List<Ticket> { new Ticket(Guid.NewGuid(), Guid.NewGuid(), 1.00m) } }
            };

            string consoleOutput;

            using (var output = new StringWriter())
            {
                Console.SetOut(output);

                // Act
                _ticketService.DisplayTicketsPurchased(players);
                
                consoleOutput = output.ToString();
            }

            // Assert
            Assert.Contains("2 other CPU players have also purcahsed tickets:", consoleOutput);
            Assert.Contains("*Player 1 has purchased 1 tickets", consoleOutput);
            Assert.Contains("*Player 2 has purchased 2 tickets", consoleOutput);
            Assert.Contains("*Player 3 has purchased 1 tickets", consoleOutput);
        }
    }
}
