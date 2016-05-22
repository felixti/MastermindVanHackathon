using System.Collections.Generic;

namespace MastermindVanHackathon.CrossCutting
{
    public interface IMastermindMatch
    {
        IDictionary<string, int> MatchGuessWithCode(string code, string guess);
    }
}