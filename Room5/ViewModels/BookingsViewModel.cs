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
        private string _info;
        public string Info
        {
            get
            {
                if (LessonOutput > 0)
                {
                    return LessonOutput.ToString();
                }
                else
                {
                    return Title;
                }
            }
            set
            {
                _info = value;
            }
        }
        public BookingsViewModel(string title)
        {
           // Title = title;
            Model = new Booking();
            Model.Title = title;
           
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

        public string Id
        {
            get => Model.BookingId.ToString();
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

    }
}
