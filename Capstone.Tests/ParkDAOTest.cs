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
    public class ParkDAOTest
    {
        

        private TransactionScope tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
        private string _connectionString;
        private int mypark = 0;

        //Constructor
        public ParkDAOTest()
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

                conn.Open();
                
                //New Test Park
                cmd = new SqlCommand("INSERT INTO park  Values ('Test Park', 'Ohio', '2019-02-20', 2000,2,'this is a park')", conn);
                cmd.ExecuteNonQuery();

                //Get Park Count
                cmd = new SqlCommand("SELECT COUNT(*) FROM park;", conn);
                mypark = (int)cmd.ExecuteScalar();


            }

        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }

        [TestMethod]
        public void GetParksTest()
        {
            //Arrange
            ParkDAO parkSqlDAL = new ParkDAO(_connectionString);

            //Act
            IList<Park> parks = parkSqlDAL.GetParks();

            //Assert
            Assert.AreEqual(mypark, parks.Count);
            Assert.AreEqual("Test Park", parks[parks.Count -1].Name);
        }





    }
}
