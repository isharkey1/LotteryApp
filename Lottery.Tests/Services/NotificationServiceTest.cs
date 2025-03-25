using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using Lottery.Services;
using Lottery.Models;
using Lottery.Dtos;
using Lottery.Enums;

namespace Lottery.Tests.Services
{
    public class NotificationServiceTest
    {
        private readonly NotificationService _notificationService;
        private readonly Casino _casino;

        public NotificationServiceTest()
        {
            _casino = new Casino { Name = "Test Casino", Revenue = 100.00m };
            _notificationService = new NotificationService(_casino);
        }

        [Fact]
        public void ShowWelcomeMessage_ShouldPrintCorrectMessage()
        {
            // Arrange
            var player = new Player(Guid.NewGuid(), "Player 1", isCPU: false);
            decimal ticketPrice = 1.00m;
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            _notificationService.ShowWelcomeMessage(player, ticketPrice);

            // Assert
            var output = consoleOutput.ToString();
            Assert.Contains($"Welcome to the {_casino.Name} Lottery, {player.Name}!", output);
            Assert.Contains($"* Your digital balance: ${player.Balance:F2}", output);
            Assert.Contains($"* Ticket Price: ${ticketPrice:F2} each", output);
        }

        [Fact]
        public void ShowGameConclusionMessage_ShouldPrintHouseRevenue()
        {
            // Arrange
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            _notificationService.ShowGameConclusionMessage();

            // Assert
            var output = consoleOutput.ToString();
            Assert.Contains($"House Revenue: ${_casino.Revenue:F2}", output);
        }
    }
}
