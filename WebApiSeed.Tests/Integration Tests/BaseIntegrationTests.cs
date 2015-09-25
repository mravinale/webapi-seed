namespace WebApiSeed.Tests
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using Castle.Windsor;
    using Configuration.Ioc;
    using Data.Configuration.EF;
    using Data.Configuration.EF.Interfaces;
    using Data.Domain;
    using Dtos;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class BaseIntegrationTests
    {
        protected IWindsorContainer Container; // IOC to obtain the db context
        protected WebApiSeedDbContext DbContext; // Db context to access the database directly
        protected TestServer Server; // API Server

        #region Constants


        #endregion

        [TestInitialize]
        public void Initialize()
        {
            Server = TestServer.Create<Startup>();
            Container = TestsBootstrapper.InitializeContainer();
            DbContext = (WebApiSeedDbContext)Container.Resolve<IDbContext>();
            DbContext.Database.Initialize(true);
        }

        [TestCleanup]
        public void AssemblyCleanup()
        {
            DbContext.Database.Delete();
        }
    }
}