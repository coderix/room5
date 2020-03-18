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

        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Equals(Booking other)
        {
            return
                this.Id == other.Id;
        }
    }
}
