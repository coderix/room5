using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Room5.Models;

namespace Room5.ViewModels
{
    public class RoomsViewModel
    {
        public RoomsViewModel(Room model)
        {
            if (model == null)
            {
                Model = new Room();
               
            }
            else
            {
                Model = model;
            }
           
            
            
        }

        /// <summary>
        /// The underlying Room model. Internal so it is 
        /// not visible to the RadDataGrid. 
        /// </summary>
        internal Room Model { get; set; }

        /// <summary>
        /// Gets or sets whether the underlying model has been modified. 
        /// This is used when sync'ing with the server to reduce load
        /// and only upload the models that changed.
        /// </summary>
        internal bool IsModified { get; set; }

        public string RoomId
        {
            get => Model.RoomId.ToString();
        }
        /// <summary>
        /// Gets or sets the Room's first name.
        /// </summary>
        public string RoomName
        {
            get => Model.RoomName;
            set
            {
                if (value != Model.RoomName)
                {
                    Model.RoomName = value;
                    IsModified = true;
                }
            }
        }

        private List<Booking> _roomBookins;
       

     
      
    }
}
