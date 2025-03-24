using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.Models;

namespace Lottery.Interfaces
{
    public interface IPlayerService
    {
        List<Player> SetupPlayers();
    }
}
