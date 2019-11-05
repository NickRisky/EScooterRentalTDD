using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalLibrary;
using System.Linq;
using Moq;
using ScooterRentalLibrary.Services;
using ScooterRentalLibrary.Interfaces;
using ScooterRentalLibrary.Entities;

namespace ScooterRentTests
{
    /// <summary>
    /// Summary description for ReportServiceTests
    /// </summary>
    [TestClass]
    public class ReportServiceTests
    {
        public ReportServiceTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CalculateIncome_TestMethod()
        {
            //Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetRentalRecords()).Returns(new List<RentalRecord>
            { 
                 new RentalRecord(new Scooter("test1", 0.10m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.00m},
                 new RentalRecord(new Scooter("test2", 0.12m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.20m},
                 new RentalRecord(new Scooter("test3", 0.15m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.50m},
            });
            var reportService = new ReportService(mockRepository.Object);                        
            //Act
            var result = reportService.CalculateIncome(false,null);
            //Assert            
            Assert.AreEqual(3.70m, result);
        }

        [TestMethod]
        public void CalculateIncome_TestMethod_IncludeActive()
        {
            //Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetRentalRecords()).Returns(new List<RentalRecord>
            {
                 new RentalRecord(new Scooter("test1", 0.10m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.00m},
                 new RentalRecord(new Scooter("test2", 0.12m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.20m},
                 new RentalRecord(new Scooter("test3", 0.15m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.50m},
                 new RentalRecord(new Scooter("test4", 0.15m)){ StartTime = DateTime.Now.AddMinutes(-10), Total = 0m},
            });
            var reportService = new ReportService(mockRepository.Object);
            //Act
            var result = reportService.CalculateIncome(true,null);
            //Assert            
            Assert.AreEqual(5.20m, Math.Round(result, 2));
        }

        [TestMethod]
        public void CalculateIncome_TestMethod_Year()
        {
            //Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.GetRentalRecords()).Returns(new List<RentalRecord>
            {
                 new RentalRecord(new Scooter("test0", 0.10m)){ EndTime = new DateTime(2018, 12, 2), StartTime = new DateTime(2018,12,1), Total = 20m},
                 new RentalRecord(new Scooter("test1", 0.10m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.00m},
                 new RentalRecord(new Scooter("test2", 0.12m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.20m},
                 new RentalRecord(new Scooter("test3", 0.15m)){ EndTime = DateTime.Now.AddMinutes(10), Total = 1.50m},
                 new RentalRecord(new Scooter("test4", 0.15m)){ StartTime = DateTime.Now.AddMinutes(-10), Total = 0m},
            });
            var reportService = new ReportService(mockRepository.Object);
            //Act
            var result = reportService.CalculateIncome(true, 2018);
            //Assert            
            Assert.AreEqual(20m, result);
        }
    }
}
