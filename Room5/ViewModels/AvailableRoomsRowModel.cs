
using System.Collections.Generic;

namespace Room5.ViewModels
{
    public class AvailableRoomsRowModel
    {
        public int LessonNumber { get; set; }
        public List<BookingsViewModel> Monday { get; set; }
        public List<BookingsViewModel> Tuesday { get; set; }
        public List<BookingsViewModel> Wednesday { get; set; }
        public List<BookingsViewModel> Thursday { get; set; }
        public List<BookingsViewModel> Friday { get; set; }
        public List<BookingsViewModel> Saturday { get; set; }
        public List<BookingsViewModel> Sunday { get; set; }
    }
}
