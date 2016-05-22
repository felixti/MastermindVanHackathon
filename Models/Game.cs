using MastermindVanHackathon.CrossCutting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MastermindVanHackathon.Models
{
    public class Game
    {
        public ObjectId _id { get; set; }

        private readonly IMastermindMatch _matermindMatch;

        public Game(IMastermindMatch mastermindMatch) : this()
        {
            _matermindMatch = mastermindMatch;
        }

        protected Game()
        {
            Colors = new string[] { "R", "B", "G", "Y", "O", "P", "C", "M" };
            CodeLength = Colors.Length;
            Gamekey = "";
            _matermindMatch = _matermindMatch == null ? new MastermindMatch() : _matermindMatch;
        }

        public string[] Colors { get; private set; }
        public int CodeLength { get; protected set; }
        public String Gamekey { get; protected set; }
        public string Whosturn { get; protected set; }
        public bool Solved { get; protected set; }
        public string Code { get; protected set; }
        public Player CodeBreaker { get; protected set; }
        public Player CodeMaker { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public bool IsSolved()
        {
            return Solved;
        }

        public void MatchCode(Player currentPlayer)
        {
            var match = _matermindMatch.MatchGuessWithCode(Code, currentPlayer.Guess);
            Solved = match["match"] == 1;
            currentPlayer.AddPastResult(match["exact"], match["near"]);
        }

        public virtual void SetCodeBreaker(Player player)
        {
            if (string.IsNullOrEmpty(player.Gamekey))
            {
                CodeBreaker = player;
                CodeBreaker.SetGamekey(Gamekey);
            }
        }

        public virtual void SetCodeMaker(Player player)
        {
            if (string.IsNullOrEmpty(player.Gamekey))
            {
                CodeMaker = player;
                CodeMaker.SetGamekey(Gamekey);
            }
        }

        public void GenerateCode()
        {
            Code = MastermindRandomize.RandomGuess(Colors);
        }


        public void SetupNewGame()
        {
            Gamekey = TokenGenerator.GenerateToken();
            CreatedAt = DateTime.Now;
        }

        public dynamic Result { get; private set; }



        public void SetResult(Player player)
        {
            var lastResult = player.PastResults.Last();
            UpdatedAt = DateTime.Now;
            Result = new { lastResult.Exact, lastResult.Near };
        }

        public bool Timeout()
        {
            var expired =  DateTime.Now.Subtract(CreatedAt).Seconds > 300;

            return expired;
        }

        public int TimeTaken()
        {
            return UpdatedAt.Value.Subtract(CreatedAt).Seconds;
        }

        public void SetWhosturn(string userName)
        {
            Whosturn = userName;
        }
    }
}