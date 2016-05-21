using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
