using MastermindVanHackathon.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
