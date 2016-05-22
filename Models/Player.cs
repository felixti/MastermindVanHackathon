namespace MastermindVanHackathon.Models
{
    public class Player
    {
        public Player(string user)
        {
            this.User = user;
        }

        public string User { get; protected set; }
    }
}