namespace WebApiSeed.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Data.Domain;

    using Dtos;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Services;

    using Common.User;

    [TestClass]
    public class UserServicesTests : BaseServiceUnitTests
    {
        [TestMethod]
        [TestCategory("UnitTests")]
        public void DeleteUserTest()
        {
            var user = new User
            {
                Id = 1
            };

            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => user);
            UserRepositoryMock.Setup(repository => repository.DeleteUser(It.IsAny<int>()));

            var userServices = new UserServices(DbContext, null, null);

            var result = userServices.DeleteUser(It.IsAny<int>());

            UserRepositoryMock.Verify(repository => repository.DeleteUser(It.IsAny<int>()), Times.Once);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void DeleteUserNonExistentTest()
        {
            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => null);
            UserRepositoryMock.Setup(repository => repository.DeleteUser(It.IsAny<int>()));

            var userServices = new UserServices(DbContext, null, null);

            var result = userServices.DeleteUser(It.IsAny<int>());

            UserRepositoryMock.Verify(repository => repository.DeleteUser(It.IsAny<int>()), Times.Never);

            Assert.IsFalse(result);
        }

        

        [TestMethod]
        [TestCategory("UnitTests")]
        public void GetAllUsersTest()
        {
            var list = new List<User>
            {
                new User(),
                new User()
            };
            UserRepositoryMock.Setup(repository => repository.GetAllUsers()).Returns(() => list);

            var userServices = new UserServices(DbContext, MappingEngine, null);

            var result = userServices.GetAllUsers();

            Assert.AreEqual(list.Count, result.Count());
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void GetUserByIdTest()
        {
            var user = new User
            {
                Id = 1
            };
            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => user);
            var userServices = new UserServices(DbContext, MappingEngine, null);
            var result = userServices.GetUserById(It.IsAny<int>());
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void GetUserByIdNonExistentTest()
        {
            var user = new User
            {
                Id = 1
            };
            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => null);
            var userServices = new UserServices(DbContext, MappingEngine, null);
            var result = userServices.GetUserById(It.IsAny<int>());
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void GetUserByUsernameTest()
        {
            var user = new User
            {
                Id = 1
            };
            UserRepositoryMock.Setup(repository => repository.FindUserByUserName(It.IsAny<string>())).Returns(() => user);
            var userServices = new UserServices(DbContext, MappingEngine, null);
            var result = userServices.GetUserByUsername(It.IsAny<string>());
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void GetUserByUsernameNonExistentTest()
        {
            var user = new User
            {
                Id = 1
            };
            UserRepositoryMock.Setup(repository => repository.FindUserByUserName(It.IsAny<string>())).Returns(() => null);
            var userServices = new UserServices(DbContext, MappingEngine, null);
            var result = userServices.GetUserByUsername(It.IsAny<string>());
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void LogoutTest()
        {
            var user = new User
            {
                Id = 1
            };
            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => user);
            UserRepositoryMock.Setup(repository => repository.SaveOrUpdateUser(It.IsAny<User>()));

            var userServices = new UserServices(DbContext, MappingEngine, null);
            var result = userServices.LogoutUser(It.IsAny<int>());

            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void UpdateUserTest()
        {
            var userDto = new UserDto
            {
                Id = 1,
                Gender = "Male",
                UserName = "NewUsername",
            };

            var user = new User
            {
                Id = 1,
                Gender = Gender.Female,
                UserName = "OldUsername",
            };

            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => user);
            UserRepositoryMock.Setup(repository => repository.SaveOrUpdateUser(It.IsAny<User>()));

            var userServices = new UserServices(DbContext, MappingEngine, null);
            var result = userServices.UpdateUser(userDto);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dto);
            Assert.AreEqual(userDto.UserName, result.Dto.UserName);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void UpdateUserNotificationsFalseTest()
        {
            var userDto = new UserDto
            {
                Id = 1,
                Gender = "Male",
                UserName = "NewUsername",
            };

            var user = new User
            {
                Id = 1,
                Gender = Gender.Female,
                UserName = "OldUsername",
            };

            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => user);
            UserRepositoryMock.Setup(repository => repository.SaveOrUpdateUser(It.IsAny<User>()));

            var userServices = new UserServices(DbContext, MappingEngine, null);
            var result = userServices.UpdateUser(userDto);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dto);
            Assert.AreEqual(userDto.UserName, result.Dto.UserName);
        }

        [TestMethod]
        [TestCategory("UnitTests")]
        public void UpdateUserUsernameExistsTest()
        {
            var userDto = new UserDto
            {
                Id = 1,
                Gender = "Male",
                UserName = "NewUsername",
            };

            var user = new User
            {
                Id = 1,
                Gender = Gender.Female,
                UserName = null,
            };

            UserRepositoryMock.Setup(repository => repository.FindUserById(It.IsAny<int>())).Returns(() => user);
            UserRepositoryMock.Setup(repository => repository.FindUserByUserName(It.IsAny<string>())).Returns(() => new User());
            UserRepositoryMock.Setup(repository => repository.SaveOrUpdateUser(It.IsAny<User>()));

            var userServices = new UserServices(DbContext, MappingEngine, null);
            var userServiceResult = userServices.UpdateUser(userDto);

            Assert.IsNotNull(userServiceResult);
            Assert.IsNotNull(userServiceResult.Dto);
            Assert.AreEqual(user.UserName, userServiceResult.Dto.UserName);
            Assert.AreEqual(UserServiceResultEnum.UsernameExists, userServiceResult.Result.Enum);
        }
    }
}