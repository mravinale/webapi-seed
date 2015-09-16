namespace WebApiSeed.Tests.Unit_Tests.Repository_Tests
{
    using Common.Helpers.Interfaces;
    using Data.Configuration.EF.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    public class BaseRepositoryUnitTests
    {
        protected Mock<IDbContext> ContextMock { get; set; }
        protected Mock<ILoggingHelper> LoggingHelperMock { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            ContextMock = new Mock<IDbContext>();
            LoggingHelperMock = new Mock<ILoggingHelper>();
        }
    }
}