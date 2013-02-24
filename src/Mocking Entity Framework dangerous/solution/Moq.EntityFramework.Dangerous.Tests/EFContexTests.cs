using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.EntityFramework.Dangerous.Model;
using Moq.EntityFramework.Dangerous.Repositories;


namespace Moq.EntityFramework.Dangerous.Tests
{
    [TestClass]
    public class EFContexTests
    {
        [TestMethod]
        public void FakeContext_GetAllUsers()
        {

            // Create Fake user Data, this will replace our Database entries.
            var userList = new List<User>
            {
                new User() { Id= 1, Name = "Frederik", PictureUrl = "test" },
                new User() { Id=2, Name = "Admin", PictureUrl = "test2" }
            };

            // Create a new instance of the MoqObjectSet class, this will be our mocked replacement for the IObjectSet Users property
            IDbSet<User> moqUserObjectSet = new MoqDbSet<User>(userList);

            // Create mock for IObjectContext and bind the Users property to our mocked "MoqObjectSet" implementation
            var mockContext = new Mock<IUserContext>();
            mockContext.Setup(m => m.Users).Returns(moqUserObjectSet);

            // Tests
            var shopRepository = new UserRepository(mockContext.Object);

            Assert.AreEqual(shopRepository.GetAll().Count, 2);
        }


        [TestMethod]
        public void FakeContext_GetWhereImageNotExists()
        {

            // Create Fake user Data, this will replace our Database entries.
            var userList = new List<User>
            {
                new User() { Id= 1, Name = "Frederik", PictureUrl = "test" },
                new User() { Id=2, Name = "Admin", PictureUrl = "test2" }
            };

            // Create a new instance of the MoqObjectSet class, this will be our mocked replacement for the IObjectSet Users property
            IDbSet<User> moqUserObjectSet = new MoqDbSet<User>(userList);

            // Create mock for IObjectContext and bind the Users property to our mocked "MoqObjectSet" implementation
            var mockContext = new Mock<IUserContext>();
            mockContext.Setup(m => m.Users).Returns(moqUserObjectSet);

            // Tests
            var shopRepository = new UserRepository(mockContext.Object);

            Assert.AreEqual(shopRepository.GetWhereImageNotExists().Count, 2);
        }



        [TestMethod]
        public void DbContext_GetAllUsers()
        {
            var ctx = new UserContext();
            // Create Fake user Data, this will replace our Database entries.
            var userList = new List<User>
            {
                new User() { Id= 1, Name = "Frederik", PictureUrl = "test" },
                new User() { Id=2, Name = "Admin", PictureUrl = "test2" }
            };


            // Tests
            var shopRepository = new UserRepository(ctx);

            Assert.AreEqual(shopRepository.GetAll().Count, 2);
        }

        [TestMethod]
        public void DbContext_GetWhereImageNotExists()
        {
            var ctx = new UserContext();

            // Tests
            var shopRepository = new UserRepository(ctx);

            Assert.AreEqual(shopRepository.GetWhereImageNotExists().Count, 2);
        }


        [TestInitialize]
        public void SeedDatabase()
        {
            var ctx = new UserContext();

            // Create Fake user Data, this will replace our Database entries.
            var userList = new List<User>
            {
                new User() { Id= 1, Name = "Frederik", PictureUrl = "test" },
                new User() { Id=2, Name = "Admin", PictureUrl = "test2" }
            };

            foreach (var user in userList)
            {
                ctx.Users.Add(new User() { Name = user.Name, PictureUrl = user.PictureUrl });
            }

            ctx.SaveChanges();
        }

        [TestCleanup]
        public void CleanDatabase()
        {
            var ctx = new UserContext();

            // Create Fake user Data, this will replace our Database entries.
            var userList = new List<User>
            {
                new User() { Id= 1, Name = "Frederik", PictureUrl = "test" },
                new User() { Id=2, Name = "Admin", PictureUrl = "test2" }
            };

            foreach (var user in userList)
            {
                var dbUser = ctx.Users.SingleOrDefault(u => u.Name.Equals(user.Name) && u.PictureUrl.Equals(user.PictureUrl));
                ctx.Users.Remove(dbUser);
            }

            ctx.SaveChanges();
        }
    }
}
