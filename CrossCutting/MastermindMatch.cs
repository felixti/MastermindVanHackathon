using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.CrossCutting
{
    public class MastermindMatch : IMastermindMatch
    {
        public IDictionary<string, int> MatchGuessWithCode(string code, string guess)
        {
            IDictionary<string, int> result = new Dictionary<string, int>();
            int qttExactFounded = 0;
            int qttFounded = 0;

            var charsCode = code.ToArray();
            var charsGuess = guess.ToArray();

            for (int i = 0; i < code.Length; i++)
                if (charsCode[i] == charsGuess[i])
                    qttExactFounded++;


            result.Add("exact", qttExactFounded);
        }
    }
}
