using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMS.challenge.common;
using CMS.challenge.api;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.challenge.data.Entities;
using CMS.challenge.data.Cache;
using Moq;
using CMS.challenge.api.Controllers;
using System.Threading.Tasks;

namespace CMS.challenge.common.Tests
{
    [TestClass()]
    public class ValidationClassTests
    {
        /*
         * UNABLE TO GET THESE TESTS TO WORK DUE TO RELIANCE ON THE DATABASE
        private Mock<ISimpleObjectCache<Guid, User>> _objectCacheMock;

        [TestMethod()]
        public async Task EmailExistsTest_DoesExist()
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Email = "test@test.com";

            Action<object> action = (object obj) =>
            {
            };

            Task<IEnumerable<User>> task = new Task();

            var mockRepo = new Mock<ISimpleObjectCache<Guid, User>>();
            mockRepo.Setup(x => x.GetAllAsync());

            var guiID = Guid.NewGuid();

            var controller = new UserController(mockRepo.Object);
            //await controller.CreateUserAsync(user);

            //task.
            bool actual = ValidationClass.EmailExists("test@test.com", , guiID);

            bool actual = true;
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public async Task UserExistsTestAsync_DoesExist()
        {
            /*User user = new User();
            user.Id = Guid.NewGuid();
            user.Email = "test@test.com";

            _objectCacheMock = new Mock<ISimpleObjectCache<Guid, User>>();
            _objectCacheMock.Setup
            //await _simpleObjectCache.AddAsync(user.Id, user);

            bool actual = ValidationClass.UserExists(user.Id, _objectCacheMock);
            bool expected = true;

            Assert.AreEqual(expected, actual);
        }*/

        [TestMethod()]
        public void IsValidFirstNameTest_Valid()
        {
            // Arrange
            string firstname = "test";

            //Act
            bool actual = ValidationClass.IsValidFirstName(firstname);

            // Assert
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidFirstNameTest_NotValid()
        {
            // Arrange
            string firstname = "sdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdts";

            //Act
            bool actual = ValidationClass.IsValidFirstName(firstname);

            // Assert
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidLastNameTest_Valid()
        {
            // Arrange
            string lastname = "test";

            //Act
            bool actual = ValidationClass.IsValidLastName(lastname);

            // Assert
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidLastNameTest_NotValid()
        {
            // Arrange
            string lastname = "sdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdtssdgetdgfheudhftegdts";

            //Act
            bool actual = ValidationClass.IsValidLastName(lastname);

            // Assert
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidEmailTest_Valid()
        {
            // Arrange
            string email = "test@test.com";

            //Act
            bool actual = ValidationClass.IsValidEmail(email);

            // Assert
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidEmailTest_NotValid()
        {
            // Arrange
            string email = "test";

            //Act
            bool actual = ValidationClass.IsValidEmail(email);

            // Assert
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidDateOfBirthTest_Valid()
        {
            // Arrange
            DateTime dateOfBirth = DateTime.Parse("2021-12-16T09:57:37.952Z");

            //Act
            bool actual = ValidationClass.IsValidDateOfBirth(dateOfBirth);

            // Assert
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsValidDateOfBirthTest_NotValid()
        {
            //TODO Get date to work properly
            // Arrange
            string dateOfBirth = "test";
            /* try
             {
                 var parsedDate = DateTime.Parse(dateOfBirth);
             }
             catch(FormatException e)
             {

             }*/

            //Act
            //bool actual = ValidationClass.IsValidDateOfBirth(parsedDate);

            // Assert
            bool expected = false;
            bool actual = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Is18OrOlderTest_IsOlder()
        {
            // Arrange
            DateTime dateOfBirth = DateTime.Parse("2001-12-16T09:57:37.952Z");

            //Act
            bool actual = ValidationClass.Is18OrOlder(dateOfBirth);

            // Assert
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Is18OrOlderTest_IsNotOlder()
        {
            // Arrange
            DateTime dateOfBirth = DateTime.Parse("2021-12-16T09:57:37.952Z");

            //Act
            bool actual = ValidationClass.Is18OrOlder(dateOfBirth);

            // Assert
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateAgeTest()
        {
            // Arrange
            DateTime dateOfBirth = DateTime.Parse("2001-12-16T09:57:37.952Z");

            //Act
            int actual = ValidationClass.CalculateAge(dateOfBirth);

            // Assert
            int expected = 20;
            Assert.AreEqual(expected, actual);
        }
    }
}