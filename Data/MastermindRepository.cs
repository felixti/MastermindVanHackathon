using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MastermindVanHackathon.Models;
using MongoDB.Driver;

namespace MastermindVanHackathon.Data
{
    public class MastermindRepository : IMastermindRepository
    {
        private readonly MongoConnection _connection;
        private readonly string _nameCollection = "Games";
        private readonly IMongoCollection<Game> _gameCollection;
        public MastermindRepository(MongoConnection Connection)
        {
            _connection = Connection;
            _gameCollection = _connection.Db.GetCollection<Game>(_nameCollection);
        }
        private void CreateCollection()
        {
            _connection.Db.CreateCollection(_nameCollection);
        }

        public Game GetGamebyGamekey(string gameKey)
        {
            return _gameCollection.Find(game => game.Gamekey == gameKey).ToList().First();
        }

        public IMongoCollection<Game> GetGameColletction()
        {
            throw new NotImplementedException();
        }

        private bool HasCollection()
        {
            var isCreated =_gameCollection.Count(_ => true) >= 0;

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
            if (!this.HasCollection())
            {
                this.CreateCollection();
            }
        }
    }
}
