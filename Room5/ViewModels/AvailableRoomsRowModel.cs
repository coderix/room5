
using System.Collections.Generic;

namespace Room5.ViewModels
{
    public class AvailableRoomsRowModel
    {
        public int LessonNumber { get; set; }
        public List<BookingsViewModel> Monday { get; set; }
    }
}
