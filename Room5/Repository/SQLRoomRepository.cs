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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Room> GetAsync(Guid id)
        {
            return await _db.Rooms
                .AsNoTracking()
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
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Room> UpsertAsync(Room Room)
        {
            var current = await _db.Rooms.FirstOrDefaultAsync(x => x.RoomId == Room.RoomId);
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
            var Room = await _db.Rooms.FirstOrDefaultAsync(x => x.RoomId == id);
            if (null != Room)
            {
                _db.Rooms.Remove(Room);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAllRoomsAsync()
        {
            var rooms = await _db.Rooms.ToListAsync();
            if (null != rooms)
            {
                foreach (var room in rooms)
                {
                    _db.Rooms.Remove(room);
                }
               
                await _db.SaveChangesAsync();
            }
        }
    }
}
