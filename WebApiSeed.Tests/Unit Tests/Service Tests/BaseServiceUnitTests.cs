namespace WebApiSeed.Tests
{
    using System.Text.RegularExpressions;
    using AutoMapper;
    using Castle.Windsor;
    using Common.Helpers.Interfaces;
    using Common.Utils.Interfaces;
    using Data.Repositories.Interfaces;
    using Infrastructure.Helpers.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Interfaces;

    public class BaseServiceUnitTests
    {
        private IWindsorContainer _container;
        protected Mock<IUserRepository> UserRepositoryMock { get; set; }
        protected Mock<IRetryExecuter> RetryExecuterMock { get; set; }
        protected Mock<ISecurityHelper> SecurityHelperMock { get; set; }

        protected IMapper MappingEngine { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _container = Bootstrapper.InitializeContainer();
            RetryExecuterMock = new Mock<IRetryExecuter>();
            UserRepositoryMock = new Mock<IUserRepository>();
            SecurityHelperMock = new Mock<ISecurityHelper>();
            MappingEngine = _container.Resolve<IMapper>();
        }
    }
}