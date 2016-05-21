using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class NewGame
    {
        public NewGame()
        {
            Colors = new string[] { "R", "B", "G", "Y", "O", "P", "C", "M" };
            CodeLength = Colors.Length;
            Gamekey = Guid.NewGuid();
        }

        public string[] Colors { get; private set; }
        public int CodeLength { get; private set; }
        public Guid Gamekey { get; private set; }
        public int NumGuesses { get; private set; }
        public string[] PastResults { get; private set; }
        public bool Solved { get; private set; }

        public void ChangeSolve(bool solved)
        {
            this.Solved = solved;
        }

        public bool IsSolved()
        {
            return Solved;
        }
    }
}
