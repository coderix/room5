using Room5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Room5.Repository
{
    class SQLRoom5Repository : IRoom5Repository
    {
        private DbContextOptions<Room5Context> _dbOptions;

        public SQLRoom5Repository(DbContextOptionsBuilder<Room5Context> dbOptionsBuilder)
        {
            _dbOptions = dbOptionsBuilder.Options;
            using (var db = new Room5Context(_dbOptions))
            {
              //  db.Database.Migrate();
                db.Database.EnsureCreated();
            }
        }

       // public Room5Context room5Context = new Room5Context(_dbOptions);
        public IRoomRepository Rooms => new SQLRoomRepository(new Room5Context(_dbOptions));
        public IBookingRepository Bookings => new SQLBookingRepository(new Room5Context(_dbOptions));
    }
}
