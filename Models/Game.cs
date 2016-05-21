using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class Game
    {

        public Game(string code) : this()
        {
            this.Code = code;
        }
        public Game()
        {
            Colors = new string[] { "R", "B", "G", "Y", "O", "P", "C", "M" };
            CodeLength = Colors.Length;
            Gamekey = "";
            PastResults = new List<PastResult>();
        }

        public string[] Colors { get; private set; }
        public int CodeLength { get; protected set; }
        public String Gamekey { get; protected set; }
        public int NumGuesses { get; protected set; }
        public IList<PastResult> PastResults { get; protected set; }
        public bool Solved { get; protected set; }
        public string Code { get; protected set; }
        public string Guess { get; protected set; }
        public bool IsSolved()
        {
            return Solved;
        }

        public void MatchCode()
        {

        }

    }
}
