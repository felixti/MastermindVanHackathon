using MastermindVanHackathon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.AppServices
{
    public interface IMastermindAppService
    {
        Game StartGame(Player player);
        Game TryGuessCode(Guess guess);
        bool IsFinished(string gamekey);
    }
}
