using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using Lottery.Services;
using Lottery.Models;
using Lottery.Dtos;
using Lottery.Enums;
using System.Linq;

namespace Lottery.Tests.Services
{
    public class PrizeServiceTest
    {
        private readonly PrizeService _prizeService;
        private readonly Casino _casino;

        public PrizeServiceTest()
        {
            _casino = new Casino { Revenue = 00.00m };
            _prizeService = new PrizeService(_casino);
        }

        [Fact]
        public void RunPrizeDraws_ShouldReturnWinningTickets()
        {
            // Arrange
            var playerId1 = Guid.NewGuid();
            var playerId2 = Guid.NewGuid();
            var players = new List<Player>
            {
                new Player(playerId1, "Player 1", false)
                {
                    Tickets = new List<Ticket>
                    {
                        new Ticket(Guid.NewGuid(), playerId1, 1.00m)
                    }
                },
                new Player(playerId2, "Player 2", false)
                {
                    Tickets = new List<Ticket>
                    {
                        new Ticket(Guid.NewGuid(), playerId2, 1.00m)
                    }
                }
            };

            // Act
            var winningTickets = _prizeService.RunPrizeDraws(players);

            // Assert
            Assert.NotEmpty(winningTickets);
        }

        [Fact]
        public void RunGrandPrizeDraw_ShouldAwardSingleGrandPrize()
        {
            // Arrange
            var playerId1 = Guid.NewGuid();
            var playerId2 = Guid.NewGuid();
            var players = new List<Player>
            {
                new Player(playerId1, "Player 1", false)
                {
                    Tickets = new List<Ticket>
                    {
                        new Ticket(Guid.NewGuid(), playerId1, 1.00m)
                    }
                },
                new Player(playerId2, "Player 2", false)
                {
                    Tickets = new List<Ticket>
                    {
                        new Ticket(Guid.NewGuid(), playerId2, 1.00m)
                    }
                }
            };

            var remainingTickets = players.SelectMany(p => p.Tickets).ToList();
            var winningTickets = new List<WinningTicketDto>();

            // Act
            _prizeService.RunGrandPrizeDraw(players, remainingTickets, winningTickets, 50.00m, PrizeTier.GrandPrize);

            // Assert
            Assert.Single(winningTickets); 
            Assert.Equal(50.00m, winningTickets[0].PrizeAmount); 
        }

        [Fact]
        public void RunMultipleWinnersDraw_ShouldAwardSecondTierPrizes()
        {
            // Arrange
            var playerId1 = Guid.NewGuid();
            var playerId2 = Guid.NewGuid();
            var players = new List<Player>
            {
                new Player(playerId1, "Player 1", false)
                {
                    Tickets = new List<Ticket>
                    {
                        new Ticket(Guid.NewGuid(), playerId1, 1.00m)
                    }
                },
                new Player(playerId2, "Player 2", false)
                {
                    Tickets = new List<Ticket>
                    {
                        new Ticket(Guid.NewGuid(), playerId2, 1.00m)
                    }
                }
            };

            var remainingTickets = players.SelectMany(p => p.Tickets).ToList();
            var winningTickets = new List<WinningTicketDto>();

            // Act
            _prizeService.RunMultipleWinnersDraw(players, remainingTickets, winningTickets, 10.00m, 2, PrizeTier.SecondTier);

            // Assert
            Assert.Equal(2, winningTickets.Count); 
            Assert.Equal(5.00m, winningTickets[0].PrizeAmount);
        }

        [Fact]
        public void GetRemainingTickets_ShouldReturnNonWinningTickets()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket(Guid.NewGuid(), Guid.NewGuid(), 1.00m) { HasWon = false },
                new Ticket(Guid.NewGuid(), Guid.NewGuid(), 1.00m) { HasWon = true },
                new Ticket(Guid.NewGuid(), Guid.NewGuid(), 1.00m) { HasWon = false }
            };

            var players = new List<Player>
            {
                new Player(Guid.NewGuid(), "Player 1", false) { Tickets = tickets }
            };

            // Act
            var remainingTickets = _prizeService.GetRemainingTickets(players);

            // Assert
            Assert.Equal(2, remainingTickets.Count); 
        }

        [Fact]
        public void RunPrizeDraws_ShouldUpdateCasinoRevenue()
        {
            // Arrange
            var playerId1 = Guid.NewGuid();
            var players = new List<Player>
            {
                new Player(playerId1, "Player 1", false)
                {
                    Tickets = new List<Ticket>
                    {
                        new Ticket(Guid.NewGuid(), playerId1, 1.00m)
                    }
                }
            };

            decimal initialRevenue = _casino.Revenue;

            // Act
            _prizeService.RunPrizeDraws(players);

            // Assert
            Assert.True(_casino.Revenue > initialRevenue); 
        }
    }
}
