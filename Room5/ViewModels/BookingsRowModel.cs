﻿
namespace Room5.ViewModels
{
   
    public class BookingsRowModel
    {
        
        public int LessonNumber { get; set; }
        public BookingsViewModel Monday { get; set; }
        public BookingsViewModel Tuesday { get; set; }
        public BookingsViewModel Wednesday { get; set; }
        public BookingsViewModel Thursday { get; set; }
        public BookingsViewModel Friday { get; set; }
        public BookingsViewModel Saturday { get; set; }
        public BookingsViewModel Sunday { get; set; }
    }
}
