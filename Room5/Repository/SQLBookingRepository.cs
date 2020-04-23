using Room5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Room5.Repository
{
    public class SQLBookingRepository : IBookingRepository
    {
        private Room5Context _db;

        public SQLBookingRepository(Room5Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Booking>> GetAsync()
        {
            return await _db.Bookings
               // .OrderBy(a => a.BookingName)
                  .ToListAsync();
        }

        public async Task<Booking> GetAsync(Guid id)
        {
            return await _db.Bookings
                .FirstOrDefaultAsync(x => x.BookingId == id);
        }

        public IEnumerable<Booking> Get(Room room)
        {
            return _db.Bookings
                  .Where(booking => booking.RoomId == room.RoomId)
                  .ToList();
        }

        public async Task<IEnumerable<Booking>> GetAsync(string value)
        {
            string[] parameters = value.Split(' ');
            return await _db.Bookings
                /*.Where(x =>
                    parameters.Any(y =>
                        x.BookingName.StartsWith(y) ))
                .OrderByDescending(x =>
                    parameters.Count(y =>
                        x.BookingName.StartsWith(y) ))*/
                .ToListAsync();
        }

        public async Task<Booking> UpsertAsync(Booking Booking)
        {
            var current = await _db.Bookings.FirstOrDefaultAsync(x => x.BookingId == Booking.BookingId);
            if (null == current)
            {
                _db.Bookings.Add(Booking);
            }
            else
            {
                _db.Entry(current).CurrentValues.SetValues(Booking);
            }
            await _db.SaveChangesAsync();
            return Booking;
        }

        public async Task DeleteAsync(Guid id)
        {
            var Booking = await _db.Bookings.FirstOrDefaultAsync(x => x.BookingId == id);
            if (null != Booking)
            {
                _db.Bookings.Remove(Booking);
                await _db.SaveChangesAsync();
            }
        }

        public async Task <IEnumerable<Booking>> GetFutureBookingsAsync(Booking booking)
        {
            return await _db.Bookings
                .Where(b =>
                b.RoomId == booking.RoomId && b.Day == booking.Day && b.Lesson == booking.Lesson)
                .ToListAsync();
        }
    }
}
