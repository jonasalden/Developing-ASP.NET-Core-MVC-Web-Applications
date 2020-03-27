using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AspNetCore_10_TestingTroubleShooting.Controllers;
using AspNetCore_10_TestingTroubleShooting.Models;
using AspNetCore_10_TestingTroubleShooting.Services;
using AspNetCore_10_TestingTroubleShooting.Tests.FakeRepositories;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Logging;

namespace AspNetCore_10_TestingTroubleShooting.Tests
{
    [TestClass]
    public class ShirtControllerTest
    {
        [TestMethod]
        public void IndexModelShouldContainAllShirts()
        {
            IShirtRepository fakeShirtRepository = new FakeShirtRepository();

            var mockLogger = new Mock<ILogger<ShirtController>>();
            ShirtController shirtController = new ShirtController(fakeShirtRepository, mockLogger.Object);

            var viewResult = shirtController.Index() as ViewResult;
            var shirts = viewResult.Model as List<Shirt>;

            Assert.AreEqual(shirts.Count, 3);

        }
    }
}
