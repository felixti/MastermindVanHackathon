using MastermindVanHackathon.Models;

namespace MastermindVanHackathon.AppServices
{
    public interface IMastermindAppService
    {
        Game StartGame(Player player);

        Game TryGuessCode(Guess guess);

        bool IsFinished(string gamekey, out string resultMassage);
    }
}