using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MastermindVanHackathon.Models;
using MastermindVanHackathon.Data;

namespace MastermindVanHackathon.AppServices
{
    public class MastermindMultiplayerAppService : IMastermindMultiplayerAppService
    {
        private readonly IMastermindRepository _mastermindRepository;
        public MastermindMultiplayerAppService(IMastermindRepository mastermindRepository)
        {
            _mastermindRepository = mastermindRepository;
        }

        public dynamic Join(Player player, string roomId)
        {
            dynamic ret;
            try
            {
                MultiplayerGame multiplayerGame = null;
                if ((multiplayerGame = _mastermindRepository.GetRoomWaitingforPlayer(roomId)) == null)
                {
                    Room room = new Room(roomId);
                    multiplayerGame = new MultiplayerGame(room);
                    multiplayerGame.SetupNewGame();
                    multiplayerGame.SetPlayer(player);
                    _mastermindRepository.Insert(multiplayerGame);

                    ret = new { Gamekey = multiplayerGame.Gamekey, RoomId = room.RoomId, Message = "You are joined. Waiting for the second player!" };
                }
                else
                {
                    multiplayerGame.SetPlayer(player);
                    multiplayerGame.Room.SetRoomIsFull();
                    _mastermindRepository.Replace(multiplayerGame);

                    ret = new { Gamekey = multiplayerGame.Gamekey, RoomId = multiplayerGame.Room.RoomId, Message = "You are joined. Please wait for start the game." };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        public dynamic SetSecretCode(string userName, string roomId, string code)
        {
            var multiplayerGame = _mastermindRepository.GetGamebyRoomAndUserName(userName, roomId);
            multiplayerGame.SetCodeSecret(code);
            _mastermindRepository.Replace(multiplayerGame);

            return new { IsFinished = true, Message = "Secret code is set. Good luck for Code Breker, Enjoy!" };
        }

        public dynamic Guess(string gamekey, string guessCode)
        {
            dynamic ret;
            string message = string.Empty;
            var currentGame = _mastermindRepository.GetMultiplayerGamebyGamekey(gamekey);

            if (IsFinished(currentGame, out message))
                return new { Message = message };

            currentGame.CodeBreaker.SetGuess(guessCode);
            currentGame.MatchCode(currentGame.CodeBreaker);
            currentGame.SetResult(currentGame.CodeBreaker);
            _mastermindRepository.Replace(currentGame);

            if (currentGame.IsSolved())
            {
                ret = new
                {
                    currentGame.CodeLength,
                    FurtherInstructions = "Solve the challenge to see this!",
                    currentGame.Colors,
                    currentGame.Gamekey,
                    currentGame.CodeBreaker.Guess,
                    currentGame.CodeBreaker.NumGuesses,
                    currentGame.CodeBreaker.PastResults,
                    Result = "",
                    currentGame.Solved,
                    TimeTaken = currentGame.TimeTaken(),
                    currentGame.CodeBreaker.User
                };
            }
            else
            {
                ret = new
                {
                    currentGame.CodeLength,
                    currentGame.Colors,
                    currentGame.Gamekey,
                    currentGame.CodeBreaker.Guess,
                    currentGame.CodeBreaker.NumGuesses,
                    currentGame.CodeBreaker.PastResults,
                    currentGame.Result,
                    currentGame.Solved
                };
            }

            return ret;
        }

        private bool IsFinished(MultiplayerGame currentGame, out string resultMessage)
        {
            resultMessage = "";
            if (currentGame.IsSolved())
                resultMessage = "Game is solved. Congratulations!";
            if (currentGame.Timeout())
                resultMessage = "Games has expired. Please, start over!";
            if (currentGame.CodeBreaker.TryLimitExpired())
                resultMessage = "You already reached the limit of tries. Please, start over!";

            return currentGame.IsSolved() || currentGame.Timeout() || currentGame.CodeBreaker.TryLimitExpired();
        }
    }
}
