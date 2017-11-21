using System.Collections.Generic;
using System.Collections.Specialized;
using App;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Tests.Api
{
    [TestClass]
    public class StartupTests
    {
        private Dictionary<string, string> _configurations;
        private Startup _startup;

        [TestInitialize]
        public void Init()
        {
            _configurations = new Dictionary<string, string>();
            var root = new ConfigurationBuilder()
                .AddInMemoryCollection(_configurations)
                .Build();

            _startup = new Startup(root);
        }

        [TestMethod]
        public void ConfigureServicesShouldAddMvc()
        {
            var services = Substitute.For<IServiceCollection>();

            _startup.ConfigureServices(services);

            services.Received().AddMvc();
            services.ReceivedWithAnyArgs().AddSwaggerGen(null);
        }
    }
}