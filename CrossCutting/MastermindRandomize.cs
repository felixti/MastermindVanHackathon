using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.CrossCutting
{
    public static class MastermindRandomize
    {
        private static Random _random;

        static MastermindRandomize()
        {
            _random = new Random();
        }
        public static string RandomGuess(string[] colors)
        {

            List<string> result = new List<string>();

            for (int i = 0; i < colors.Length; i++)
            {
                result.Add(colors[_random.Next(colors.Length)]);
            }

            return result.ToString();
        }
    }
}
