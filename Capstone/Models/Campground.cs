using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        /// <summary>
        /// The Campground Id
        /// </summary>
        public int CampgroundId { get; set; }

        /// <summary>
        /// The Park Id where the camp is located
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// The Campground Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Open from Month
        /// </summary>
        public int OpenFrom { get; set; }

        /// <summary>
        /// Gets the name month
        /// </summary>
        public string OpenFromString { get
            {
                return getMonthName(OpenFrom);
            }
        }
        /// <summary>
        /// Open to Month
        /// </summary>
        public int OpenTo { get; set; }

        /// <summary>
        /// / Gets the name of month
        /// </summary>
        public string OpenToString { get
            {
                return getMonthName(OpenTo);
            }
        }

        /// <summary>
        /// The daily fee
        /// </summary>
        public decimal DailyFee { get; set; }


        /// <summary>
        /// Returns the name of the month from an integer 1 through 12
        /// </summary>
        /// <param name="month"></param>
        /// <returns>string</returns>
        private string getMonthName(int month)
        {
            string result = "";

            if (month == 1) result = "January";
            else if (month == 2) result = "February";
            else if (month == 3) result = "March";
            else if (month == 4) result = "April";
            else if (month == 5) result = "May";
            else if (month == 6) result = "June";
            else if (month == 7) result = "July";
            else if (month == 8) result = "August";
            else if (month == 9) result = "September";
            else if (month == 10) result = "October";
            else if (month == 11) result = "November";
            else if (month == 12) result = "December";

            return result;
        }

    }
}
