using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Core.Models
{
    public class Room : IEquatable<Room>
    {
        public string RoomName { get; set; }
       
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Equals(Room other)
        {
            return
                this.RoomName == other.RoomName ;
        }
    }
}
