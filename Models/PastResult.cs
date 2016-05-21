namespace MastermindVanHackathon.Models
{
    public class PastResult
    {
        public PastResult(string guess, int exact, int near)
        {
            this.Guess = guess;
            this.Exact = exact;
            this.Near = near;
        }

        public int Exact { get; protected set; }
        public string Guess { get; private set; }
        public int Near { get; protected set; }
    }
}