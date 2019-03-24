using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class ReservationDAO
    {
        #region Member Variables
        private string _connectionString;
        #endregion
        #region Constructor
        public ReservationDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Inserts reservation into the database and returns the confirmation ID
        /// </summary>
        /// <param name="site"></param>
        /// <param name="name"></param>
        /// <param name="arrival"></param>
        /// <param name="departure"></param>
        /// <returns>Confirmation ID</returns>
        public int MakeReservation(Site site, string name, DateTime arrival, DateTime departure)
        {
            int result = 0;
            DateTime today = DateTime.Now;

            //Define Sql Statement 
            string SqlMakeReservation = $"INSERT INTO reservation (site_id, name, from_date, to_date, create_date)" +
                                        $"Values (@site_id, @name, @from_date, @to_date, @today)" +
                                        $"SELECT CAST(SCOPE_IDENTITY() as int)";

            //Create the connection object
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //Open Connection
                conn.Open();

                //Create my Command Object
                SqlCommand cmd = new SqlCommand(SqlMakeReservation, conn);
                cmd.Parameters.AddWithValue("@site_id", site.SiteId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@from_date", arrival.Date);
                cmd.Parameters.AddWithValue("@to_date", departure.Date);
                cmd.Parameters.AddWithValue("@today", today);

                //Execute Command
                result = Convert.ToInt32(cmd.ExecuteScalar());                
             
            }

            return result;
        }


        /// <summary>
        /// Returns a list of all reservation in the given park
        /// </summary>
        /// <param name="park"></param>
        /// <returns>list</returns>
        public IList<Reservation> GetReservationsInPark(Park park)
        {
            List<Reservation> result = new List<Reservation>();
            //Define Sql Statement
            string SqlReturnAllReservations = $"SELECT * FROM reservation " +
                                              $"JOIN site ON reservation.site_id = site.site_id" +
                                              $"JOIN campground ON site.campground_id = site.campground_id" +
                                              $"WHERE campground.park_id = @park_id";

            //Create the connection object
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //Open Connection
                conn.Open();

                //Create my Command Object
                SqlCommand cmd = new SqlCommand(SqlReturnAllReservations, conn);
                cmd.Parameters.AddWithValue("@park_id", park.ParkId);

                //Execute Command
                SqlDataReader reader = cmd.ExecuteReader();

                //Loop through the set
                while (reader.Read())
                {
                    //Populate objects to return
                    Reservation r = new Reservation();
                    r.ReservationId = Convert.ToInt32(reader["reservation_id"]);
                    r.SiteId = Convert.ToInt32(reader["site_id"]);
                    r.Name = Convert.ToString(reader["name"]);
                    r.FromDate = Convert.ToDateTime(reader["from_date"]);
                    r.ToDate = Convert.ToDateTime(reader["to_date"]);
                    r.CreateDate = Convert.ToDateTime(reader["create_date"]);

                    //Add the newly created object to the list
                    result.Add(r);
                }
            }
            //Return the list of Objects
            return result;
        }

        #endregion
    }
}
