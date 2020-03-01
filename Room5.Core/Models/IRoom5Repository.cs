using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Core.Models
{
    public interface IRoom5Repository
    {
        IRoomRepository Rooms { get; }
    }
}
