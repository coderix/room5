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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Booking> GetAsync(Guid id)
        {
            return await _db.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Booking> UpsertAsync(Booking Booking)
        {
            var current = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == Booking.Id);
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
            var Booking = await _db.Bookings.FirstOrDefaultAsync(x => x.Id == id);
            if (null != Booking)
            {
                _db.Bookings.Remove(Booking);
                await _db.SaveChangesAsync();
            }
        }
    }
}
