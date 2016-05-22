using MongoDB.Driver;
using System.Configuration;

namespace MastermindVanHackathon.Data
{
    public class MongoConnection
    {
        private readonly string _connectionString = ConfigurationManager.AppSettings["mongo_url"].ToString();
        public IMongoDatabase Db { get; private set; }

        public MongoConnection()
        {
            MongoClient client = new MongoClient(_connectionString);
            Db = client.GetDatabase("MastermindVanHackathonDb");
        }
    }
}