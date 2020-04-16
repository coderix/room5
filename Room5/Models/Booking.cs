using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Models
{
    public class Booking : IEquatable<Booking>
    {
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
        public Guid BelongsTo { get; set; }
        public string Title { get; set; }
        public int Day { get; set; }
        public int Lesson { get; set; }
        public bool IsRecurrent { get; set; }

        /// <summary>
        /// Type of recurrency, daily, weekly ....
        /// </summary>
        public int Repeat { get; set; }

        public DateTime StartDate  { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string Remarks { get; set; }

        public bool Equals(Booking other)
        {
            return
                BookingId == other.BookingId;
        }
    }
}
