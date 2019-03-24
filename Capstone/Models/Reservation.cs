using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        /// <summary>
        /// The Reservation Id
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The Site Id the reservation is on
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// The Reservation Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The starting date of the reservation
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// The ending date of the reservation
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// The date the reservation was created
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
