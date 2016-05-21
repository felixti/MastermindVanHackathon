using MastermindVanHackathon.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Data
{
    public interface IMastermindRepository
    {
        void CreateCollection();
        void Insert(Game game);
        void Replace(Game game);
        IMongoCollection<Game> GetGameColletction();
        Game GetGamebyGamekey(string gameKey);
        bool HasCollection();

    }
}
