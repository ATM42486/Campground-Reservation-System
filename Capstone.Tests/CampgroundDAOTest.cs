using Capstone.DAO;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundDAOTest
    {
        

        private TransactionScope tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
        private string _connectionString;
        private int parkID = 0;

        //Constructor
        public CampgroundDAOTest()
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("Project");

        }

        // Set up the database before each test        
        [TestInitialize]
        public void Initialize()
        {
            
            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd;

                //Open Connection
                conn.Open();
                
                //New Test Park
                cmd = new SqlCommand("INSERT INTO park  Values ('Test Park', 'Ohio', '2019-02-20', 2000,2,'this is a park');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT park_ID from park where name = 'Test Park';", conn);
                parkID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand($"INSERT INTO campground VALUES ({parkID}, 'Test camp', 01, 02, 100.00);", conn);
                cmd.ExecuteNonQuery();


            }

        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }


        [TestMethod]
        public void GetCampgroundsTest()
        {
            //Arrange
            ParkDAO parkSqlDAL = new ParkDAO(_connectionString);
            CampgroundDAO campgroundDAL = new CampgroundDAO(_connectionString);


            //Act
            //gets last created park
            IList<Park> parks = parkSqlDAL.GetParks();
            Park testPark = parks[parks.Count - 1];
           
            IList<Campground> campgrounds = campgroundDAL.GetCampgrounds(testPark);

            //Assert
            Assert.AreEqual(1, campgrounds.Count);
            Assert.AreEqual("Test camp", campgrounds[0].Name);
            Assert.AreEqual(parkID, campgrounds[0].ParkId);
            Assert.AreEqual(100, campgrounds[0].DailyFee);
        }



    }
}
