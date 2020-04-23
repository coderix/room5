using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Models
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Returns all bookings. 
        /// </summary>
        Task<IEnumerable<Booking>> GetAsync();

        /// <summary>
        /// Returns all bookings with a data field matching the start of the given string. 
        /// </summary>
        Task<IEnumerable<Booking>> GetAsync(string search);

        /// <summary>
        /// Returns all bookings of a room
        /// </summary>
        IEnumerable<Booking> Get(Room room);
        /// <summary>
        /// 
        /// Returns the booking with the given id. 
        /// </summary>
        Task<Booking> GetAsync(Guid id);

        /// <summary>
        /// Adds a new booking if the booking does not exist, updates the 
        /// existing booking otherwise.
        /// </summary>
        Task<Booking> UpsertAsync(Booking booking);

        /// <summary>
        /// Deletes a booking.
        /// </summary>
        Task DeleteAsync(Guid bookingId);

        /// <summary>
        /// Returns all bookings with a data field matching the start of the given string. 
        /// </summary>
        Task<IEnumerable<Booking>> GetFutureBookingsAsync(Booking booking);
    }
}
