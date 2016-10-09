using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelWebSite.Controllers;
using System.Web.Mvc;
using Moq;
using TravelWebSite.Models;
using System.Collections.Generic;

namespace TravelWebSite.Tests.Controllers
{
    [TestClass]
    public class DocumentControllerTests
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {
        
            var mock = new Mock<IDocRepository>();
            mock.Setup(a => a.GetDocumentsList()).Returns(new List<Document>());
            DocumentsController controller = new DocumentsController(mock.Object);

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void CreatePostAction_ModelError()
        {
            // arrange
            var mock = new Mock<IDocRepository>();
            Document doc = new Document();
            DocumentsController controller = new DocumentsController(mock.Object);
            controller.ModelState.AddModelError("Name", "Название модели не установлено");
            // act
            ViewResult result = controller.Create(doc,null) as ViewResult;
            // assert
            Assert.IsNotNull(result);
          
        }

        [TestMethod]
        public void CreatePostAction_RedirectToIndexView()
        {
            // arrange
            string expected = "Index";
            var mock = new Mock<IDocRepository>();
            Document doc = new Document();
            DocumentsController controller = new DocumentsController(mock.Object);
            // act
            RedirectToRouteResult result = controller.Create(doc,null) as RedirectToRouteResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreatePostAction_SaveModel()
        {
            // arrange
            var mock = new Mock<IDocRepository>();
            Document doc = new Document();
            DocumentsController controller = new DocumentsController(mock.Object);
            // act
            RedirectToRouteResult result = controller.Create(doc,null) as RedirectToRouteResult;
            // assert
            mock.Verify(a => a.Create(doc));
            
        }

    }
}

