using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room5.Helpers
{
    public class Week
    {
        public DateTime Monday;
        public DateTime Tuesday;
        public DateTime Wednesday;
        public DateTime Thursday;
        public DateTime Friday;
        public DateTime Saturday;
        public DateTime Sunday;

        public Week(DateTime monday)
        {
            Monday = monday;
            Tuesday = Monday.AddDays(1);
            Wednesday = Monday.AddDays(2);
            Thursday = Monday.AddDays(3);
            Friday = Monday.AddDays(4);
            Saturday = Monday.AddDays(5);
            Sunday = Monday.AddDays(6);
        }

        public string getShortDate(DateTime day)
        {
            return ($"{day.Day}.{day.Month}.");
        }
    }
}
