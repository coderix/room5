using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Room5.Helpers
{
    public static class DateHelper
    {
      //  public static DateTime today = DateTime.Now;

        /// <summary>
        /// Gets or sets the Date of this week's Monday.
        /// </summary>
        public static DateTime FirstMonday(DateTime today)
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

        public static Week getWeek(DateTime monday)
        {
            return new Week(monday);
        }

        public static Week NextWeek(DateTime monday)
        {
            return new Week(monday.AddDays(7));
        }

        public static Week PreviousWeek(DateTime monday)
        {
            return new Week(monday.AddDays(-7));
        }

        static void dates()
        {
            DateTime date1 = DateTime.Now;
            Console.WriteLine(date1.ToString());
            Console.WriteLine($"{date1.Day}.{date1.Month}.");
            Console.WriteLine("The day of the week for {0:d} is {1}.", date1, (int)date1.DayOfWeek);

            DateTime dateToDisplay = new DateTime(2009, 6, 1, 8, 42, 50);
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            // Change culture to de-DE.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Console.WriteLine("Displaying short date for {0} culture:",
                              Thread.CurrentThread.CurrentCulture.Name);
            Console.WriteLine("   {0} (Short Date String)",
                              dateToDisplay.ToShortDateString());
            // Display using 'd' standard format specifier to illustrate it is
            // identical to the string returned by ToShortDateString.
            Console.WriteLine("   {0} ('d' standard format specifier)",
                              dateToDisplay.ToString("d"));
            Console.WriteLine();


            // Restore original culture.
            Thread.CurrentThread.CurrentCulture = originalCulture;

            DateTime today = DateTime.Now;
            DateTime firstMonday = DateTime.Now;
            Console.WriteLine("The day of the week for {0} is {1}.", today.DayOfWeek, (int)today.DayOfWeek);

            //Get the date of this week's monday
            switch ((int)today.DayOfWeek)
            {

                case 0: firstMonday = today.AddDays(-6); break; //Sunday
                case 1: firstMonday = today; break; //Monday
                case 2: firstMonday = today.AddDays(-1); break; // Tuesday
                case 3: firstMonday = today.AddDays(-2); break;
                case 4: firstMonday = today.AddDays(-3); break;
                case 5: firstMonday = today.AddDays(-4); break;
                case 6: firstMonday = today.AddDays(-5); break;
                default:
                    break;
            }
         
            Console.WriteLine("The day of the week for this week's monday {0} is {1},  {2}.", firstMonday.DayOfWeek, (int)firstMonday.DayOfWeek, firstMonday.ToShortDateString());

        }


    }
}
