using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Services;

namespace Lottery.Tests.Services
{
    public class PlayerServiceTest
    {
        private readonly PlayerService _playerService;

        public PlayerServiceTest()
        {
            _playerService = new PlayerService(); 
        }

        [Fact]
        public void SetupPlayers_ShouldReturnAtLeastOnePlayer()
        {
            // Act
            var players = _playerService.SetupPlayers();

            // Assert
            Assert.NotEmpty(players);
            Assert.Equal("Player 1", players[0].Name);
        }

        [Fact]
        public void SetupPlayers_ShouldReturnCpuPlayersInRange()
        {
            // Act
            var players = _playerService.SetupPlayers();

            // Assert
            int cpuPlayerCount = players.Count(p => p.IsCPU);
            Assert.InRange(cpuPlayerCount, 9, 14);
        }

        [Fact]
        public void SetupPlayers_ShouldHaveCorrectCpuPlayerNames()
        {
            // Act
            var players = _playerService.SetupPlayers();

            // Assert
            var cpuPlayers = players.Where(p => p.IsCPU).ToList();
            for (int i = 0; i < cpuPlayers.Count; i++)
            {
                Assert.Equal($"Player {i + 2}", cpuPlayers[i].Name);
            }
        }
    }
}
