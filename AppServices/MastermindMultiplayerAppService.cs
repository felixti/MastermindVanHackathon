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

        public dynamic Guess(string guess)
        {
            throw new NotImplementedException();
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

                    ret = new { RoomId = room.RoomId, Message = "You are joined. Waiting for the second player!" };
                }
                else
                {
                    multiplayerGame.SetPlayer(player);
                    multiplayerGame.Room.SetRoomIsFull();
                    _mastermindRepository.Replace(multiplayerGame);

                    ret = new { RoomId = multiplayerGame.Room.RoomId,  Message = "You are joined. Please wait for start the game." };
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

            return new { Message = "Secret code is set. Good luck for Code Breker, Enjoy!" };
        }

        public dynamic StartGame(string code)
        {
            throw new NotImplementedException();
        }
    }
}
