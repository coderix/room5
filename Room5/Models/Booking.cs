using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Models
{
    public class Booking : IEquatable<Booking>
    {
        public string Title { get; set; }

        public Guid BookingId { get; set; } = Guid.NewGuid();
        public int RoomId { get; set; }
        public Room room { get; set; }
        public bool Equals(Booking other)
        {
            return
                BookingId == other.BookingId;
        }
    }
}
