using MyApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Api
{
    [TestClass]
    [TestCategory("Controllers")]
    public class ProbesControllerTests
    {
        private ProbesController _controller;

        [TestInitialize]
        public void Init()
        {
            _controller = new ProbesController();
        }

        [TestMethod]
        [TestProperty("Resource", "Probe")]
        public void TestGetReturnVersion()
        {
            var result = _controller.Get();

            result.As<ObjectResult>()
                  .ShouldHaveStatusCode(200)
                  .ShouldReturnValue("15.3.0");
        }
    }
}
