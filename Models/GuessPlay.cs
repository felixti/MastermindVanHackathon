using MastermindVanHackathon.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class GuessPlay : Game
    {
        public GuessPlay(string gamekey, string code)
            : base(code)
        {
            this.Gamekey = gamekey;
            this.CodeLength = code.Length;
            this.NumGuesses++;
        }


        public dynamic Result { get; private set; }

        private void AddPastResult()
        {
            PastResult pastResult = new PastResult(Guess);
            pastResult.SetGuessResult();
        }

        public void GenerateGuess()
        {
            this.Guess = MastermindRandomize.RandomGuess(this.Colors);
            AddPastResult();
        }


    }
}
