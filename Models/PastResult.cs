namespace MastermindVanHackathon.Models
{
    public class PastResult
    {
        public PastResult(string guess)
        {
            this.Guess = guess;
        }

        public int Exact { get; protected set; }
        public string Guess { get; private set; }
        public int Near { get; protected set; }

        public void SetGuessResult(int exact, int near)
        {
            this.Exact = exact;
            this.Near = near;
        }
    }
}