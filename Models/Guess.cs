namespace MastermindVanHackathon.Models
{
    public class Guess
    {
        public Guess(string code, string gameKey)
        {
            Code = code;
            GameKey = gameKey;
        }

        public string Code { get; protected set; }
        public string GameKey { get; protected set; }
    }
}