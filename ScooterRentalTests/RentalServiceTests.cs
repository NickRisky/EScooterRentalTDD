using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScooterRentalLibrary;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalLibrary.Services;
using ScooterRentalLibrary.Entities;
using ScooterRentalLibrary.Interfaces;

namespace ScooterRentTests
{
    [TestClass]
    public class RentalServiceTests
    {
        public RentalServiceTests()
        {
            //
            // TODO: Add constructor logic here
            //
            testScooter = "testScooter";
            MockRepository.Instance.AddScooter(testScooter, 0.10m);
            rentalService = new RentalService(MockRepository.Instance);
        }
        private RentalService rentalService { get; set; }
        private string testScooter { get; set; }
        [TestMethod]
        public void Rent_TestMethod()
        {
            //Arrange            
            //Act
            var result = rentalService.Rent("testScooter");
            //Assert            
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Rent_TestMethodIfEmpty()
        {
            //Arrange                        
            //Act
            var ex = Assert.ThrowsException<ArgumentNullException>(() => rentalService.Rent(string.Empty));
            //Assert            
            Assert.AreEqual("Value cannot be null.\r\nParameter name: scooterId", ex.Message);
        }

        [TestMethod]
        public void StopRent_TestMethod()
        {
            //Arrange          
            //Act
            rentalService.Rent(testScooter);
            var result = rentalService.StopRent(testScooter);
            //Assert            
            Assert.AreEqual(true, result > 0m);
        }

        [TestMethod]
        public void StopRent_TestMethodIfNull()
        {
            //Arrange            
            //Act
            rentalService.Rent(testScooter);
            //Assert            
            var ex = Assert.ThrowsException<ScooterNotFoundException>(() => rentalService.StopRent(testScooter + "2"));
            Assert.AreEqual("Scooter not found: testScooter2", ex.Message);
        }

        [TestMethod]
        public void StopRent_TestIfScooterNotRented()
        {
            //Arrange            
            var scooter = MockRepository.Instance.GetScooterById(testScooter);
            //Act
            rentalService.Rent(testScooter);
            scooter.IsRented = false;
            var ex = Assert.ThrowsException<ScooterNotRentedException>(() => rentalService.StopRent(testScooter));
            //Assert            
            Assert.AreEqual("Scooter already stopped rent: testScooter", ex.Message);
        }

        [TestMethod]
        public void StopRent_TestMethod_CheckDay()
        {            
            //Arrange          
            var mockRepository = new Moq.Mock<IRepository>();
            mockRepository.Setup(x => x.GetScooterById("test1")).Returns(new Scooter("test1", 0.10m) { IsRented = true });
            mockRepository.Setup(x => x.GetRecordsByScooterId("test1")).Returns(new List<RentalRecord>
            {
                 new RentalRecord(new Scooter("test1", 0.10m){ IsRented = true}){StartTime = DateTime.Now.AddDays(-2).AddMinutes(-100), Total = 0m},                 
            });
            var rentService = new RentalService(mockRepository.Object);
            //Act            
            var result = rentService.StopRent("test1");
            //Assert            
            Assert.AreEqual(50m, Math.Round(result, 2));
        }
    }
}
