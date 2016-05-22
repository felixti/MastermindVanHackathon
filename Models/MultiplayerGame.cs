using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class MultiplayerGame : Game
    {
        public MultiplayerGame(Room room) : base()
        {
            Room = room;
        }

        public Roles RolePlayerWaitingFor()
        {
            return this.CodeMaker == null ? Roles.CodeBreaker : Roles.CodeMaker;
        }


        public void SetPlayer(Player player)
        {
            if (player.IsCodeBreaker())
            {
                SetCodeBreaker(player);
                CodeBreaker.PlayGame();
            }
            else
            {
                SetCodeMaker(player);
                CodeMaker.PlayGame();
            }
        }

        public void SetCodeSecret(string code)
        {
            Code = code;
        }


        public Room Room { get; protected set; }
    }
}
