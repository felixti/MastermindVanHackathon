using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class JoinViewModel
    {
        public JoinViewModel()
        {

        }

        public string Name { get; set; }
        public string Role { get; set; }
        public string RoomId { get; set; }
        public string RoomName { get; set; }
    }
}
