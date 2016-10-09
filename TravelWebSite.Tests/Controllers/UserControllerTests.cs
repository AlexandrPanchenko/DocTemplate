using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelWebSite.Controllers;
using System.Web.Mvc;
using TravelWebSite.Models;
using Moq;
using System.Collections.Generic;

namespace TravelWebSite.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {

            var mock = new Mock<IDocRepository>();
            mock.Setup(a => a.GetDocumentsList()).Returns(new List<Document>());
            UserController controller = new UserController(mock.Object);

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }


    }
}
