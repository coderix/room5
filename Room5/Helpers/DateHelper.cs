using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Helpers
{
    class DateHelper
    {
        public DateTime today = DateTime.Now;
        
        /// <summary>
        /// Gets or sets the Date of this week's Monday.
        /// </summary>
        public DateTime FirstMonday
        {
            get
            {
                //Get the date of this week's monday
                DateTime d;
                switch ((int)today.DayOfWeek)
                {
                    case 0: d = today.AddDays(-6); break; //Sunday
                    case 1: d = today; break; //Monday
                    case 2: d = today.AddDays(-1); break; // Tuesday
                    case 3: d = today.AddDays(-2); break;
                    case 4: d = today.AddDays(-3); break;
                    case 5: d = today.AddDays(-4); break;
                    case 6: d = today.AddDays(-5); break;
                    default: d = today;
                        break;
                }
                return d;
            }
        }
      

    }
}
