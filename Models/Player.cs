using System;
using System.Collections.Generic;

namespace MastermindVanHackathon.Models
{
    public class Player
    {
        public Player(string user)
        {
            this.User = user;
            PastResults = new List<PastResult>();
        }
        public string Gamekey { get; protected set; }
        public string User { get; protected set; }
        public string Guess { get; protected set; }
        public int NumGuesses { get; protected set; }
        public IList<PastResult> PastResults { get; protected set; }
        public bool Solved { get; protected set; }

        public void AddPastResult(int exact, int near)
        {
            this.PastResults.Add(new PastResult(Guess, exact, near));
        }

        public void SetGamekey(string gamekey)
        {
            this.Gamekey = gamekey;
        }

        private void SetTry()
        {
            this.NumGuesses++;
        }

        public void SetGuess(string guess)
        {
            this.Guess = guess;
            SetTry();
        }
        public bool TryLimitExpired()
        {
            return this.NumGuesses == 12;
        }
    }
}