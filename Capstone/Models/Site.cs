using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        /// <summary>
        /// The Site Id
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// The Campground Id the site is on
        /// </summary>
        public int CampgroundId { get; set; }

        /// <summary>
        /// The Site Number
        /// </summary>
        public int SiteNumber { get; set; }

        /// <summary>
        /// The Max Occupancy
        /// </summary>
        public int MaxOccupancy { get; set; }

        /// <summary>
        /// Does the site have accessibility
        /// </summary>
        public bool Accessible { get; set; }

        public string AccessibleString
        {
            get
            {
                return getBoolString(Accessible);
            }
        }


        /// <summary>
        /// Maximum length allowed for RVs
        /// </summary>
        public int MaxRVLength { get; set; }

        public string MaxRVString
        {
            get
            {
                return setZeroToNA(MaxRVLength);
            }
        }


        /// <summary>
        /// Does the site have utilities
        /// </summary>
        public bool Utilities { get; set; }

        public string UtilitiesString
        {
            get
            {
                return getBoolString(Utilities);
            }
        }

        /// <summary>
        /// Gets a bool and converts it to a yes or no
        /// </summary>
        /// <param name="b"></param>
        /// <returns>String</returns>
        private string getBoolString(bool b)
        {
            string result = "No";

            if (b == true)
            {
                result = "Yes";
            }

            return result;
        }

        /// <summary>
        /// Gets an int and converts it to N/A when 0
        /// </summary>
        /// <param name="number"></param>
        /// <returns>string</returns>
        private string setZeroToNA(int number)
        {
            string result = "";

            if (number == 0)
            {
                result = "N/A";
            }
            else
            {
                result = number.ToString();
            }
            return result;
        }

    }
}
