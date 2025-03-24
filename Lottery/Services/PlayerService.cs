using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Interfaces;
using Lottery.Models;

namespace Lottery.Services
{
    public class PlayerService : IPlayerService
    {
        private static readonly Random _random = new Random();
        private const int MinCpuPlayers = 10;
        private const int MaxCpuPlayers = 15;

        public List<Player> SetupPlayers()
        {
            var players = new List<Player>
            {
                new Player(Guid.NewGuid(), "Player 1", false) 
            };

            int cpuPlayersCount = _random.Next(MinCpuPlayers, MaxCpuPlayers + 1);

            for (int i = 2; i <= cpuPlayersCount; i++)
            {
                players.Add(new Player(Guid.NewGuid(), $"Player {i}", true));
            }

            return players;
        }
    }
}
