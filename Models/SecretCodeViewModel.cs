using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class SecretCodeViewModel
    {
        public SecretCodeViewModel()
        {

        }

        public string UserName { get; set; }
        public string RoomId { get; set; }
        public string Code { get; set; }

    }
}
