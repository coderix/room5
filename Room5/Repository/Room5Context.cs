using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Room5.Models;

namespace Room5.Repository
{
    public class Room5Context : DbContext
    {
        /// <summary>
        /// Creates a new DbContext.
        /// </summary>
        public Room5Context(DbContextOptions<Room5Context> options) : base(options)
        { }

        /// <summary>
        /// Gets the rooms DbSet.
        /// </summary>
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
