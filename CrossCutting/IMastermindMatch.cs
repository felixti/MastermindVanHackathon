using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.CrossCutting
{
    public interface IMastermindMatch
    {
        IDictionary<string, int> MatchGuessWithCode(string code, string guess);
    }
}
