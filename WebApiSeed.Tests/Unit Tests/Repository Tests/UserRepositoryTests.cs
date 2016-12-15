namespace WebApiSeed.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Data.Domain;
    using Data.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Unit_Tests.Repository_Tests;

    [TestClass]
    public class UserRepositoryTests : BaseRepositoryUnitTests
    {
        protected Mock<IDbSet<User>> UserSet;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            UserSet = new Mock<IDbSet<User>>();
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FindUserById()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var id = 1;
            var user = repository.FindUserById(id);

            Assert.IsNotNull(user);
            Assert.AreEqual(id, user.Id);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FindUserByIdNotExistent()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var id = 99;
            var user = repository.FindUserById(id);

            Assert.IsNull(user);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FindUserByUserName()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var username = "test1";
            var user = repository.FindUserByUserName(username);

            Assert.IsNotNull(user);
            Assert.AreEqual(username, user.UserName);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FindUserByUserNameNotExistent()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var username = "test99";
            var user = repository.FindUserByUserName(username);

            Assert.IsNull(user);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FindUserByToken()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var token = "AccessToken1";
            var user = repository.FindUserByToken(token);

            Assert.IsNotNull(user);
            Assert.AreEqual(token, user.AccessToken);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void FindUserByTokenNotExistent()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var token = "asd";
            var user = repository.FindUserByToken(token);

            Assert.IsNull(user);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void GetAllUsers()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var users = repository.GetAllUsers();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void DeleteUser()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());

            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);

            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var userId = 1;
            repository.DeleteUser(userId);

            UserSet.Verify(set => set.Remove(It.IsAny<User>()), Times.Once);
            ContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void DeleteUserNotExistent()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var userId = 99;
            repository.DeleteUser(userId);

            UserSet.Verify(set => set.Remove(It.IsAny<User>()), Times.Never);
            ContextMock.Verify(c => c.SaveChanges(), Times.Never);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void SaveUser()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var user = new User
            {
                UserName = "test9",
                AccessToken = "AccessToken9",
                Gender = Gender.Male,
            };

            repository.SaveOrUpdateUser(user);

            UserSet.Verify(set => set.Add(It.IsAny<User>()), Times.Once);
            ContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void UpdateUser()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var user = new User
            {
                Id = 1,
                UserName = "test1",
                AccessToken = "AccessToken1",
                Gender = Gender.Male,
            };

            repository.SaveOrUpdateUser(user);

            UserSet.Verify(set => set.Add(It.IsAny<User>()), Times.Never);
            ContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void UpdateUserNotExistent()
        {
            var data = SetupMockData();

            UserSet.Setup(set => set.Provider).Returns(data.Provider);
            UserSet.Setup(set => set.Expression).Returns(data.Expression);
            UserSet.Setup(set => set.ElementType).Returns(data.ElementType);
            UserSet.Setup(set => set.GetEnumerator()).Returns(data.GetEnumerator());
            ContextMock.Setup(c => c.Entity<User>()).Returns(UserSet.Object);
            var repository = new UserRepository(ContextMock.Object, LoggingHelperMock.Object);

            var user = new User
            {
                Id = 99,
                UserName = "test1",
                AccessToken = "AccessToken1",
                Gender = Gender.Male,
            };

            repository.SaveOrUpdateUser(user);

            UserSet.Verify(set => set.Add(It.IsAny<User>()), Times.Never);
            ContextMock.Verify(c => c.SaveChanges(), Times.Never);
        }

        private IQueryable<User> SetupMockData()
        {
            var data = new List<User>
            {
                new User
                {
                    Id = 1,
                    UserName = "test1",
                    AccessToken = "AccessToken1",
                    Gender = Gender.Female,
                },
                new User
                {
                    Id = 2,
                    UserName = "test2",
                    AccessToken = "AccessToken2",
                    Gender = Gender.Female,
                },
                new User
                {
                    Id = 3,
                    UserName = "test3",
                    AccessToken = "AccessToken3",
                    Gender = Gender.Male,
                },
                new User
                {
                    Id = 4,
                    UserName = "test4",
                    AccessToken = "AccessToken4",
                    Gender = Gender.Male,
                },
                new User
                {
                    Id = 5,
                    UserName = "test5",
                    AccessToken = "AccessToken5",
                    Gender = Gender.Male,
                },
                new User
                {
                    Id = 6,
                    UserName = "test6",
                    AccessToken = "AccessToken6",
                    Gender = Gender.Female,
                }
            };

            return data.AsQueryable();
        }
    }
}