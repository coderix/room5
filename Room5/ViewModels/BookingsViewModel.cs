using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Room5.Models;

namespace Room5.ViewModels
{
    public class BookingsViewModel
    {

        /* public BookingsViewModel(string title)
         {
            // Title = title;
             Model = new Booking();
             Model.Title = title;

         }*/

        // internal Booking Model { get; set; }

        public BookingsViewModel(

            string title = default,
            int day = 0,
            int lesson = 1,
            bool isEndless = true,
            int repeat = (int)App.Repeat.Weekly,
            DateTime startDate = new DateTime(),
            DateTime endDate = new DateTime(),
            int duration = 1,
            string remarks = default,
            Guid roomId = default,
            Guid belongsTo = default,
            Booking model = null
           
            )
        {
           
            Model = model ?? new Booking();
            Title = title;
            Day = day;
            Lesson = lesson;
           IsEndless = isEndless;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            Repeat = repeat;
            Remarks = remarks;
            RoomId = roomId;
            BelongsTo = belongsTo;

        }

        public BookingsViewModel(Booking model)
        {
            Model = model ?? new Booking();
        }
        /// <summary>
        /// The underlying Room model. Internal so it is 
        /// not visible to the RadDataGrid. 
        /// </summary>
        internal Booking Model { get; set; }

        /// <summary>
        /// Gets or sets whether the underlying model has been modified. 
        /// This is used when sync'ing with the server to reduce load
        /// and only upload the models that changed.
        /// </summary>
        internal bool IsModified { get; set; }

       
        public string BookingId
        {
            get => Model.BookingId.ToString();
        }

        public Guid RoomId
        {
            get => Model.RoomId;
            set
            {
                if (value != Model.RoomId)
                {
                    Model.RoomId = value;
                    IsModified = true;
                }
            }
        }

        public Guid BelongsTo
        {
            get => Model.BelongsTo;
            set
            {
                if (value != Model.BelongsTo)
                {
                    Model.BelongsTo = value;
                    IsModified = true;
                }
            }
        }

        private string _info;
        public string Info
        {
            get
            {
                    return Title;
             }
            set
            {
                _info = value;
                Title = value;
            }
        }
        /// <summary>
        /// Gets or sets the Room's first name.
        /// </summary>
        public string Title
        {
            get => Model.Title;
            set
            {
                if (value != Model.Title)
                {
                    Model.Title = value;
                    IsModified = true;
                }
            }
        }
        public int LessonOutput = 0;

       
        public int Day
        {
            get => Model.Day;
            set
            {
                if (value != Model.Day)
                {
                    Model.Day = value;
                    IsModified = true;
                }
            }
        }
        public string DayOutput
        {
            get
            {
                switch (Day)
                {
                    case 1: return ("Montag");
                    case 2: return ("Dienstag");
                    case 3: return ("Mittwoch");
                    case 4: return ("Donnerstag");
                    case 5: return ("Freitag");
                    case 6: return ("Samstag");
                    case 7: return ("Sonntag");

                    default:
                        return("");
                }
            }
        }

        public string DayAndLessonOutput
        {
            get
            {
                return StartDate.ToString("D", App.culture) + "   -    " + Lesson + ". Stunde";
            }
        }

        public string RoomDayAndLessonOutput
        {
            get
            {
                return RoomName + "hallo" + StartDate.ToString("D", App.culture) + "   -    " + Lesson + ". Stunde";
            }
            
        }

        public int Lesson
        {
            get => Model.Lesson;
            set
            {
                if (value != Model.Lesson)
                {
                    Model.Lesson = value;
                    IsModified = true;
                }
            }
        }

        public bool IsEndless
        {
            get => Model.IsEndless;
            set
            {
                if (value != Model.IsEndless)
                {
                    Model.IsEndless = value;
                    IsModified = true;
                }
            }
        }

        public int Repeat
        {
            get => Model.Repeat;
            set
            {
                if (value != Model.Repeat)
                {
                    Model.Repeat = value;
                    IsModified = true;
                }
            }
        }

        public DateTime StartDate
        {
            get => Model.StartDate;
            set
            {
                if (value != Model.StartDate)
                {
                    Model.StartDate = value;
                    IsModified = true;
                }
            }
        }

        public DateTime EndDate
        {
            get => Model.EndDate;
            set
            {
                if (value != Model.EndDate)
                {
                    Model.EndDate = value;
                    IsModified = true;
                }
            }
        }

       

        public int Duration
        {
            get => Model.Duration;
            set
            {
                if (value != Model.Duration)
                {
                    Model.Duration = value;
                    IsModified = true;
                }
            }
        }
        public string Remarks
        {
            get => Model.Remarks;
            set
            {
                if (value != Model.Remarks)
                {
                    Model.Remarks = value;
                    IsModified = true;
                }
            }
        }

        public string DateOutputVisibility
        {
            get
            {
                if (IsEndless == true)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }

        public string DateOutput
        {
            get
            {
              /*  if (IsEndless == false)
                {
                    // return StartDate.Day + "." + StartDate.Month + ".";
                    return StartDate.ToString("m", App.culture);
                }
                else
                {
                    return "";
                }*/
                return StartDate.ToString("m", App.culture);
            }
        }

        public string RoomName { get => _roomName;
            set
            {
                _roomName = value;
                
            }
        }

        private string _roomName;

        


    }
}
