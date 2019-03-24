using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class CampgroundDAO
    {
        #region Member Variables
        private string _connectionString;
        #endregion
        #region Constructor
        public CampgroundDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all Campgrounds in the passed Park.
        /// </summary>
        /// <param name="park"></param>
        /// <returns>List</returns>
        public IList<Campground> GetCampgrounds(int parkID)
        {
            List<Campground> result = new List<Campground>();
            //Define Sql Statement
            string SqlReturnAllCamps = $"SELECT * FROM campground WHERE park_id = @park_id " +
                                        $"ORDER BY name";

            //Create the connection object
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //Open Connection
                conn.Open();

                //Create my Command Object
                SqlCommand cmd = new SqlCommand(SqlReturnAllCamps, conn);
                cmd.Parameters.AddWithValue("@park_id", parkID);

                //Execute Command
                SqlDataReader reader = cmd.ExecuteReader();

                //Loop through the set
                while (reader.Read())
                {
                    //Populate objects to return
                    Campground c = new Campground();
                    c.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    c.ParkId = Convert.ToInt32(reader["park_id"]);
                    c.Name = Convert.ToString(reader["name"]);
                    c.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
                    c.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
                    c.DailyFee = Convert.ToDecimal(reader["daily_fee"]);

                    //Add the newly created object to the list
                    result.Add(c);
                }
            }
            //Return the list of Objects
            return result;
        }

        #endregion
    }
}
