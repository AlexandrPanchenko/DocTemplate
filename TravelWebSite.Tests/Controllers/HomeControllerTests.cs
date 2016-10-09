using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelWebSite.Controllers;
using System.Web.Mvc;

namespace TravelWebSite.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

      
       
    
    }
}
