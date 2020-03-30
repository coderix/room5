using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Models
{
    public interface IRoom5Repository
    {
        IRoomRepository Rooms { get; }
        IBookingRepository Bookings { get; }
    }
}
