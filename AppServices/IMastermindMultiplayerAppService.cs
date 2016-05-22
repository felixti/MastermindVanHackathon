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
        dynamic StartGame(string code);
        dynamic Guess(string guess);

        dynamic SetSecretCode(string userName, string roomName, string code);
    }
}
