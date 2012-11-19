using System.Collections.Generic;
using System.Data.Objects;
using Moq.EntityFramework.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq.EntityFramework;

namespace Moq.EntityFramework.Tests
{


    /// <summary>
    ///This is a test class for ShopRepositoryTest and is intended
    ///to contain all ShopRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ShopRepositoryTest
    {
        [TestMethod()]
        public void GetUsersTest()
        {
            ShopEntities context = new ShopEntities();
            ShopRepository shopRepositories = new ShopRepository(context);

            var users = shopRepositories.GetUsers();

            Assert.AreEqual(users.Count, 2);
        }

        [TestMethod()]
        public void MoqGetUsersTest()
        {
            // Create Fake user Data, this will replace our Database entries.
            var userList = new List<User>
            {
                new User() { id=1, username = "Frederik", password = "test" },
                new User() { id=2, username = "Admin", password = "test2" }
            };

            // Create a new instance of the MoqObjectSet class, this will be our mocked replacement for the IObjectSet Users property
            IObjectSet<User> moqUserObjectSet = new MoqObjectSet<User>(userList);

            // Create mock for IObjectContext and bind the Users property to our mocked "MoqObjectSet" implementation
            var mockContext = new Mock<IObjectContext>();
            mockContext.Setup(m => m.Users).Returns(moqUserObjectSet);

            // Tests
            var shopRepository = new ShopRepository(mockContext.Object);

            Assert.AreEqual(shopRepository.GetUsers().Count, 2);
            Assert.AreEqual(shopRepository.GetUserById(2).username, "Admin");

        }
    }
}
