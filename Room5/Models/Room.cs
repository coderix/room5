using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Models
{
    public class Room : IEquatable<Room>
    {
        public string RoomName { get; set; }
       
        public Guid RoomId { get; set; } = Guid.NewGuid();
        public List<Booking> Bookings { get; set; }
        public bool Equals(Room other)
        {
            return
                this.RoomName == other.RoomName ;
        }
    }
}
