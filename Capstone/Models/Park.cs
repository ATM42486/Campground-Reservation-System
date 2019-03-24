using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Park
    {
        /// <summary>
        /// The Park Id
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// The Park Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Park Location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime EstablishDate { get; set; }

        /// <summary>
        /// The Area of the park
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// Number of Visitors
        /// </summary>
        public int Visitors { get; set; }

        /// <summary>
        /// Park Description
        /// </summary>
        public string Description { get; set; }
    }
}
