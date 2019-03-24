using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class ParkDAO
    {
        #region Member Variables
        private string _connectionString;
        #endregion
        #region Constructor
        public ParkDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns a list of all the parks.
        /// </summary>
        /// <returns>List</returns>
        public IList<Park> GetParks()
        {
            List<Park> result = new List<Park>();
            //Define Sql Statement
            string SqlReturnAllParks = $"SELECT * FROM park ORDER BY name";

            //Create the connection object
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                //Open Connection
                conn.Open();

                //Create my Command Object
                SqlCommand cmd = new SqlCommand(SqlReturnAllParks, conn);

                //Execute Command
                SqlDataReader reader = cmd.ExecuteReader();

                //Loop through the set
                while (reader.Read())
                {
                    //Populate objects to return
                    Park p = new Park();
                    p.ParkId = Convert.ToInt32(reader["park_id"]);
                    p.Name = Convert.ToString(reader["name"]);
                    p.Location = Convert.ToString(reader["location"]);
                    p.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                    p.Area = Convert.ToInt32(reader["area"]);
                    p.Visitors = Convert.ToInt32(reader["visitors"]);
                    p.Description = Convert.ToString(reader["description"]);

                    //Add the newly created object to the list
                    result.Add(p);
                }
            }
            //Return the list of Objects
            return result;
        }

        #endregion
    }
}
