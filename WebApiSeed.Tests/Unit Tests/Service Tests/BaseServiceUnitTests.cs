namespace WebApiSeed.Tests
{
    using AutoMapper;

    using Common.Utils.Interfaces;

    using Infrastructure.Helpers.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Autofac;
    using Services.Interfaces;
    using Data.Configuration.EF.Interfaces;

    /// <summary>
    /// BaseServiceUnitTests Class
    /// </summary>
    public class BaseServiceUnitTests
    {
        private IContainer _container;
        protected Mock<IUserServices> UserRepositoryMock { get; set; }
        protected Mock<IRetryExecuter> RetryExecuterMock { get; set; }
        protected Mock<ISecurityHelper> SecurityHelperMock { get; set; }
        protected Mock<IDbContext> DbContextMock { get; set; }

        protected IMapper MappingEngine { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _container = Bootstrapper.InitializeContainer();
            RetryExecuterMock = new Mock<IRetryExecuter>();
            UserRepositoryMock = new Mock<IUserServices>();
            SecurityHelperMock = new Mock<ISecurityHelper>();
            DbContextMock = new Mock<IDbContext>();
            MappingEngine = _container.Resolve<IMapper>();
        }
    }
}