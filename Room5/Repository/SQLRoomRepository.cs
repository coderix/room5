﻿using Room5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Room5.Repository
{
    class SQLRoomRepository : IRoomRepository
    {
        private Room5Context _db;

        public SQLRoomRepository(Room5Context db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Room>> GetAsync()
        {
            return await _db.Rooms
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Room> GetAsync(Guid id)
        {
            return await _db.Rooms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
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
            var current = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == Room.Id);
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
            var Room = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (null != Room)
            {
                _db.Rooms.Remove(Room);
                await _db.SaveChangesAsync();
            }
        }
    }
}
