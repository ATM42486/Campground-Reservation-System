using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class SiteDAO
    {
        #region Member Variables
        private string _connectionString;
        #endregion
        #region Constructor
        public SiteDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a list the top 5 available camp sites within the given dates
        /// </summary>
        /// <param name="camp"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns>List</returns>
        public IList<Site> GetTop5Sites(int campID, DateTime FromDate, DateTime ToDate)
        {
            try
             {
                List<Site> result = new List<Site>();
                //Define Sql Statement 
                string SqlReturn5Sites = $"Select Top 5 * FROM site s " +
                                         $"WHERE s.campground_id = @camp_id AND site_id NOT in " +
                                         $"(SELECT site_id FROM reservation WHERE " +
                                         $"from_date BETWEEN @start_date AND @end_date " +
                                         $"Or to_date BETWEEN @start_date AND @end_date Or " +
                                         $"from_date BETWEEN @start_date AND @end_date AND to_date BETWEEN @end_date AND @end_date)";


                //Create the connection object
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    //Open Connection
                    conn.Open();

                    //Create my Command Object
                    SqlCommand cmd = new SqlCommand(SqlReturn5Sites, conn);
                    cmd.Parameters.AddWithValue("@camp_id", campID);
                    cmd.Parameters.AddWithValue("@start_date", FromDate.Date);
                    cmd.Parameters.AddWithValue("@end_date", ToDate.Date);

                    //Execute Command

                    SqlDataReader reader = cmd.ExecuteReader();

                    //Loop through the set
                    while (reader.Read())
                    {
                        //Populate objects to return
                        Site s = new Site();
                        s.SiteId = Convert.ToInt32(reader["site_id"]);
                        s.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Accessible = Convert.ToBoolean(reader["accessible"]);
                        s.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToBoolean(reader["utilities"]);

                        //Add the newly created object to the list
                        result.Add(s);
                    }
                }
                //Return the list of Objects
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }
        #endregion
    }
}
