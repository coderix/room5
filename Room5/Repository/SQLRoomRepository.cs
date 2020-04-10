using Room5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Room5.Repository
{
    public class SQLRoomRepository : IRoomRepository
    {
        private Room5Context _db;

        public SQLRoomRepository(Room5Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Room>> GetAsync()
        {
            return await _db.Rooms
                .OrderBy(a => a.RoomName)
                .ToListAsync();
        }

        public async Task<Room> GetAsync(Guid id)
        {
            return await _db.Rooms
                .FirstOrDefaultAsync(x => x.RoomId == id);
        }

        public async Task<IEnumerable<Room>> GetAsync(string value)
        {
            string[] parameters = value.Split(' ');
            return await _db.Rooms
                .Where(x =>
                    parameters.Any(y =>
                        x.RoomName.StartsWith(y) ))
                .OrderByDescending(x =>
                    parameters.Count(y =>
                        x.RoomName.StartsWith(y) ))
                .ToListAsync();
        }

        public async Task<Room> UpsertAsync(Room Room)
        {
            var current = await _db.Rooms
                .FirstOrDefaultAsync(x => x.RoomId == Room.RoomId);
            if (null == current)
            {
                _db.Rooms.Add(Room);
            }
            else
            {
                _db.Entry(current).CurrentValues.SetValues(Room);
            }
            await _db.SaveChangesAsync();
            return Room;
        }

        public async Task DeleteAsync(Guid id)
        {
            var room = await _db.Rooms
                .FirstOrDefaultAsync(x => x.RoomId == id);
            if (null != room)
            {
                IEnumerable<Booking> bookings = App.Repository.Bookings.Get(room);
                foreach (Booking booking in bookings)
                {
                    await App.Repository.Bookings.DeleteAsync(booking.BookingId);
                }
                _db.Rooms.Remove(room);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAllRoomsAsync()
        {
            try
            {
                var rooms = await _db.Rooms
                    .ToListAsync();
                if (null != rooms)
                {
                    foreach (var room in rooms)
                    {
                        await DeleteAsync(room.RoomId);
                       // _db.Rooms.Remove(room);
                    }

                       await _db.SaveChangesAsync();
                 }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler: {ex.InnerException.Message}");
            }
         
        }
    }
}
