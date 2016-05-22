using MastermindVanHackathon.Models;
using MongoDB.Driver;
using System;
using System.Linq;

namespace MastermindVanHackathon.Data
{
    public class MastermindRepository : IMastermindRepository
    {
        private readonly MongoConnection _connection;
        private readonly string _nameCollection = "Games";
        private readonly IMongoCollection<Game> _gameCollection;
        private readonly IMongoCollection<MultiplayerGame> _multiplayerGameCollection;

        public MastermindRepository(MongoConnection Connection)
        {
            _connection = Connection;
            _gameCollection = _connection.Db.GetCollection<Game>(_nameCollection);
            _multiplayerGameCollection = _connection.Db.GetCollection<MultiplayerGame>(_nameCollection);
        }

        private void CreateCollection()
        {
            _connection.Db.CreateCollection(_nameCollection);
        }

        public Game GetGamebyGamekey(string gameKey)
        {
            return _gameCollection.Find(game => game.Gamekey == gameKey).ToList().First();
        }

        private bool HasCollection()
        {
            var isCreated = false;
            try
            {
                isCreated = _gameCollection.Count(_ => true) >= 0;
            }
            catch { }
            

            return isCreated;
        }

        public void Insert(Game game)
        {
            _gameCollection.InsertOne(game);
        }

        public void Replace(Game game)
        {
            var originalGame = _gameCollection.Find(g => g._id == game._id)
                               .ToList().First();

            _gameCollection.ReplaceOne(c => c._id == originalGame._id, game);
        }

        public void SetupDatabase()
        {
            if (!HasCollection())
            {
                CreateCollection();
            }
        }

        public void Insert(MultiplayerGame game)
        {
            _multiplayerGameCollection.InsertOne(game);
        }
    

        public void Replace(MultiplayerGame game)
        {
            var originalGame = _multiplayerGameCollection.Find(g => g._id == game._id)
                               .ToList().First();

            _multiplayerGameCollection.ReplaceOne(c => c._id == originalGame._id, game);
        }

        public MultiplayerGame GetMultiplayerGamebyGamekey(string gameKey)
        {
            return _multiplayerGameCollection.Find(game => game.Gamekey == gameKey).ToList().First();
        }

        public MultiplayerGame GetRoomWaitingforPlayer(string roomdId)
        {
            return _multiplayerGameCollection.Find(game => (game.CodeBreaker == null || game.CodeMaker == null) && game.Room.RoomId == roomdId)
                                             .ToList().FirstOrDefault();
        }

        public MultiplayerGame GetGamebyRoomAndUserName(string userName, string roomId)
        {
            return _multiplayerGameCollection.Find(game => game.Room.RoomId == roomId && 
                                                  (game.CodeBreaker.User == userName || game.CodeMaker.User == userName))
                                             .ToList().FirstOrDefault();
        }
    }
}