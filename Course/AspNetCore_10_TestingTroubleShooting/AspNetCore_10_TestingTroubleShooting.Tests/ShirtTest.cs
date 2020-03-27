using AspNetCore_10_TestingTroubleShooting.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCore_10_TestingTroubleShooting.Tests
{
    [TestClass]
    public class ShirtTest
    {
        [TestMethod]
        public void IsGetFormattedTaxedPriceReturnsCorrectly()
        {
            var shirt = new Shirt { Price = 10F, Tax = 1.2F };
            var taxedPrice = shirt.GetFormattedTaxedPrice();

            Assert.AreEqual("$12.00", taxedPrice);

        }
    }
}
