using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Core.Models
{
    public interface IRoomRepository
    {
        /// <summary>
        /// Returns all rooms. 
        /// </summary>
         Task<IEnumerable<Room>> GetAsync();

        /// <summary>
        /// Returns all rooms with a data field matching the start of the given string. 
        /// </summary>
        Task<IEnumerable<Room>> GetAsync(string search);

        /// <summary>
        /// Returns the room with the given id. 
        /// </summary>
        Task<Room> GetAsync(Guid id);

        /// <summary>
        /// Adds a new room if the room does not exist, updates the 
        /// existing room otherwise.
        /// </summary>
        Task<Room> UpsertAsync(Room room);

        /// <summary>
        /// Deletes a room.
        /// </summary>
        Task DeleteAsync(Guid roomId);
    }
}

