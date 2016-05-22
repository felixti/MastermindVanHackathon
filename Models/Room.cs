using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVanHackathon.Models
{
    public class Room
    {
        public Room(string roomName)
        {
            RoomId = Guid.NewGuid().ToString("d");
            Name = roomName;
        }

        public string RoomId { get; protected set; }
        public string Name { get; protected set; }
        public bool IsFull { get; protected set; }

        public void SetRoomIsFull()
        {
            IsFull = true;
        }
    }
}
