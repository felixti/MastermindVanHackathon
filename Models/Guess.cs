using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class Guess
    {
        public Guess(string code, string gameKey)
        {
            this.Code = code;
            this.GameKey = gameKey;
        }

        public string Code { get; protected set; }
        public string GameKey { get; protected set; }

    }
}
