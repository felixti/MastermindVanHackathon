using MastermindVanHackathon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.AppServices
{
    public interface IMastermindMultiplayerAppService
    {
        dynamic Join(Player player, string roomId);
        dynamic Guess(string gamekey, string guessCode);
        dynamic SetSecretCode(string userName, string roomId, string code);
    }
}
