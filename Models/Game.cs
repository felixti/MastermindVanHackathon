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
            this._matermindMatch = mastermindMatch;
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

        public bool Solved { get; protected set; }

        public string Code { get; protected set; }
        public Player Player1 { get; protected set; }
        public Player Player2 { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public bool IsSolved()
        {
            return Solved;
        }

        public void MatchCode(Player currentPlayer)
        {
            var match = _matermindMatch.MatchGuessWithCode(this.Code, currentPlayer.Guess);
            this.Solved = match["match"] == 1;
            currentPlayer.AddPastResult(match["exact"], match["near"]);
        }

        public void SetPlayer1(Player player)
        {
            if (string.IsNullOrEmpty(player.Gamekey))
            {
                this.Player1 = player;
                this.Player1.SetGamekey(this.Gamekey);
            }
        }

        public void SetPlayer2(Player player)
        {
            if (string.IsNullOrEmpty(player.Gamekey))
            {
                this.Player2 = player;
                this.Player2.SetGamekey(this.Gamekey);
            }
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



        public void SetResult(Player player)
        {
            var lastResult = player.PastResults.Last();
            this.UpdatedAt = DateTime.Now;
            this.Result = new { lastResult.Exact, lastResult.Near };
        }

        public bool Timeout()
        {
            var expired =  DateTime.Now.Subtract(this.CreatedAt).Seconds > 300;

            return expired;
        }

        public int TimeTaken()
        {
            return this.UpdatedAt.Value.Subtract(this.CreatedAt).Seconds;
        }

        
    }
}