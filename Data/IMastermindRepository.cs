using MastermindVanHackathon.Models;
using MongoDB.Driver;

namespace MastermindVanHackathon.Data
{
    public interface IMastermindRepository
    {
        void Insert(Game game);

        void Replace(Game game);

        IMongoCollection<Game> GetGameColletction();

        Game GetGamebyGamekey(string gameKey);

        void SetupDatabase();
    }
}