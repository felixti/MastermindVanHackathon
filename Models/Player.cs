using System;
using System.Collections.Generic;

namespace MastermindVanHackathon.Models
{
    public class Player
    {
        public Player(string user)
        {
            User = user;
            PastResults = new List<PastResult>();
        }

        public Player(string user, Roles role)
        {
            User = user;
            PastResults = new List<PastResult>();
            Role = role;
        }

        public string Gamekey { get; protected set; }
        public string User { get; protected set; }
        public string Guess { get; protected set; }
        public int NumGuesses { get; protected set; }
        public IList<PastResult> PastResults { get; protected set; }
        public bool IsPlaying { get; protected set; }
        public bool Solved { get; protected set; }
        public Roles Role { get; protected set; }

        public void AddPastResult(int exact, int near)
        {
            PastResults.Add(new PastResult(Guess, exact, near));
        }

        public void PlayGame()
        {
            IsPlaying = true;
        }

        public void SetGamekey(string gamekey)
        {
            Gamekey = gamekey;
        }

        private void SetTry()
        {
            NumGuesses++;
        }

        public void SetGuess(string guess)
        {
            Guess = guess;
            SetTry();
        }
        public bool TryLimitExpired()
        {
            return NumGuesses == 12;
        }

        public bool IsCodeBreaker()
        {
            return Role == Roles.CodeBreaker;
        }

        public bool IsCodeMaker()
        {
            return Role == Roles.CodeMaker;
        }

        public void SetRole(string role)
        {
            Role = (Roles)Enum.Parse(typeof(Roles), role);
        }
    }
}