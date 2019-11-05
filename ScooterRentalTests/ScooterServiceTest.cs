using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScooterRentalLibrary;
using System.Collections.Generic;
using System.Linq;
using ScooterRentalLibrary.Services;

namespace ScooterRentTests
{
    [TestClass]
    public class ScooterServiceTest
    {
        [TestMethod]
        public void GetScooterById_TestMethodIfEmpty()
        {
            //Arrange
            var scooterService = new ScooterService(MockRepository.Instance);
            //Act            
            var ex = Assert.ThrowsException<ArgumentNullException>(() => scooterService.GetScooterById(string.Empty));
            //Assert                        
            Assert.AreEqual("Value cannot be null.\r\nParameter name: scooterId", ex.Message);
        }
        [TestMethod]
        public void GetScooters_TestMethod()
        {
            //Arrange
            MockRepository.Instance.RemoveScooter("testScooter");
            var scooterService = new ScooterService(MockRepository.Instance);
            //Act
            var result = scooterService.GetScooters();
            //Assert            
            Assert.AreEqual(3, result.Count);
        }

    }
}
