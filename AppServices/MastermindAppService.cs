using MastermindVanHackathon.Data;
using MastermindVanHackathon.Models;

namespace MastermindVanHackathon.AppServices
{
    public class MastermindAppService : IMastermindAppService
    {
        private readonly Game _game;
        private readonly IMastermindRepository _mastermindRepository;

        public MastermindAppService(IMastermindRepository mastermindRepository, Game game)
        {
            _game = game;
            _mastermindRepository = mastermindRepository;
            _mastermindRepository.SetupDatabase();
        }

        public bool IsFinished(string gamekey, out string resultMessage)
        {
            resultMessage = "";
            var currentGame = _mastermindRepository.GetGamebyGamekey(gamekey);

            if (currentGame.IsSolved())
                resultMessage = "Game is solved. Congratulations!";
            if (currentGame.Timeout())
                resultMessage = "Games has expired. Please, start over!";
            if (currentGame.Player1.TryLimitExpired())
                resultMessage = "You already reached the limit of tries. Please, start over!";

            return currentGame.IsSolved() || currentGame.Timeout() || currentGame.Player1.TryLimitExpired();
        }

        public Game StartGame(Player player)
        {
            _game.SetupNewGame();
            _game.SetPlayer1(player);
            _game.GenerateCode();
            _mastermindRepository.Insert(_game);

            return _game;
        }

        public Game TryGuessCode(Guess guess)
        {
            var currentGame = _mastermindRepository.GetGamebyGamekey(guess.GameKey);

            currentGame.Player1.SetGuess(guess.Code);
            currentGame.MatchCode(currentGame.Player1);
            currentGame.SetResult(currentGame.Player1);
            _mastermindRepository.Replace(currentGame);

            return currentGame;
        }
    }
}