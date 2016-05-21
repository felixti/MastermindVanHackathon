using MastermindVanHackathon.CrossCutting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class Game
    {
        public ObjectId _id { get; set; }

        private readonly IMastermindMatch _matermindMatch;

        public Game(IMastermindMatch mastermindMatch):this()
        {
            this._matermindMatch = mastermindMatch;
        }
        protected Game()
        {
            Colors = new string[] { "R", "B", "G", "Y", "O", "P", "C", "M" };
            CodeLength = Colors.Length;
            Gamekey = "";
            PastResults = new List<PastResult>();
            Players = new List<Player>();
            _matermindMatch = _matermindMatch == null ? new MastermindMatch() : _matermindMatch;
        }

        public string[] Colors { get; private set; }
        public int CodeLength { get; protected set; }
        public String Gamekey { get; protected set; }
        public int NumGuesses { get; protected set; }
        public IList<PastResult> PastResults { get; protected set; }
        public bool Solved { get; protected set; }
        public string Guess { get; protected set; }
        public string Code { get; protected set; }
        public IList<Player> Players { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsSolved()
        {
            return Solved;
        }

        public void MatchCode()
        {
            var match = _matermindMatch.MatchGuessWithCode(this.Code, this.Guess);
            this.Solved = match["match"] == 1;
            AddPastResult(match["exact"], match["near"]);

        }

        private void AddPastResult(int exact, int near)
        {
            this.PastResults.Add(new PastResult(Guess, exact, near));
        }

        public void AddPlayer(Player player)
        {
            this.Players.Add(player);
        }

        public void GenerateCode()
        {
            Code = MastermindRandomize.RandomGuess(this.Colors);
        }

        public void SetupNewGame()
        {
            this.Gamekey = TokenGenerator.GenerateToken();
            this.CreatedAt = DateTime.Now;
        }

        public dynamic Result { get; private set; }

        public void SetGuess(string guess)
        {
            this.Guess = guess;
            this.UpdatedAt = DateTime.Now;
        }

        public void SetResult()
        {
            var lastResult = this.PastResults.Last();
            this.Result = new { lastResult.Exact, lastResult.Near };
        }
    }
}
