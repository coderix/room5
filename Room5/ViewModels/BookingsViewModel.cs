using System;
using System.Collections.Generic;
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
            bool isReccurent = true,
            int repeat = 0,
            string remarks = default,
            Guid roomId = default,
            Booking model = null
           
            )
        {
            // Title = title;
            /*  Model = new Booking();
              Model.Title = title;
              Model.Day = day;
              Model.Lesson = lesson;
              Model.IsRecurrent = isReccurent;
              Model.Repeat = repeat;
              Model.Remarks = remarks;*/
            Model = model ?? new Booking();
            Title = title;
            Day = day;
            Lesson = lesson;
           IsRecurrent = isReccurent;
            Repeat = repeat;
            Remarks = remarks;
            RoomId = roomId;
            


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

        /*public Room Room
        {
            get => Model.Room;
            set
            {
                if (value != Model.Room)
                {
                    Model.Room = value;
                    IsModified = true;
                }
            }
        }*/

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
                string day;
                switch (Day)
                {
                    case 1: day = "Montag"; break;
                    case 2: day = "Dientag"; break;
                    case 3: day = "Mittwoch"; break;
                    case 4: day = "Donnerstag"; break;
                    case 5: day = "Freitag"; break;
                    case 6: day = "Samtag"; break;
                    case 7: day = "Sonntag"; break;

                    default:
                        return ("");
                }
                return day + " " + Lesson + ". Stunde";
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

        public bool IsRecurrent
        {
            get => Model.IsRecurrent;
            set
            {
                if (value != Model.IsRecurrent)
                {
                    Model.IsRecurrent = value;
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


    }
}
