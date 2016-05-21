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
            List<char> letters = new List<char>();
            List<char> exactLetters = new List<char>();

            var charsCode = code.ToArray();
            var charsGuess = guess.ToArray();

            for (int i = 0; i < code.Length; i++)
            {
                if (charsCode[i] == charsGuess[i])
                    exactLetters.Add(charsGuess[i]);

                if (charsCode.Contains(charsGuess[i]))
                    letters.Add(charsGuess[i]);
            }

            int nearCount = letters.Distinct().Except(exactLetters).Count();
            result.Add("exact", exactLetters.Count);
            result.Add("near", nearCount);
            result.Add("match", exactLetters.Count == code.Length ? 1 : 0);

            return result;
        }
    }
}
