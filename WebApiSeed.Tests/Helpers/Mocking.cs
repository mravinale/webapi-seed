namespace WebApiSeed.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using Data.Configuration.EF.Interfaces;
    using Moq;

    internal class Mocking
    {
        public static Mock<IMappingEngine> MockMapper<T, U>(U result)
        {
            var mapperMock = new Mock<IMappingEngine>();
            mapperMock.Setup(x => x.Map<T, U>(It.IsAny<T>())).Returns(result);
            return mapperMock;
        }

        public static IMock<IDbContext> MockDbContext<T>(IMock<IDbSet<T>> dbSetMock) where T : class
        {
            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(x => x.Entity<T>()).Returns(dbSetMock.Object);
            return mockContext;
        }

        public static IMock<IDbSet<T>> MockDbSet<T>(ICollection<T> list) where T : class
        {
            var mockSet = new Mock<IDbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(() => list.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(() => list.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(() => list.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator);

            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback((T x) => list.Add(x));
            mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback((T x) => list.Remove(x));
            return mockSet;
        }
    }
}