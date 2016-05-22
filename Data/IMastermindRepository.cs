using MastermindVanHackathon.Models;
using MongoDB.Driver;

namespace MastermindVanHackathon.Data
{
    public interface IMastermindRepository
    {
        void Insert(Game game);
        void Insert(MultiplayerGame game);
        void Replace(Game game);
        void Replace(MultiplayerGame game);
        Game GetGamebyGamekey(string gameKey);
        MultiplayerGame GetMultiplayerGamebyGamekey(string gameKey);
        void SetupDatabase();
        MultiplayerGame GetRoomWaitingforPlayer(string roomdId);
        MultiplayerGame GetGamebyRoomAndUserName(string userName, string roomId);
    }
}