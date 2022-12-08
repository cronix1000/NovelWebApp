using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelWebApp.Controllers;
using NovelWebApp.Data;
using NovelWebApp.Models;
using System.IO;

namespace NovelWebAppTests
{
    [TestClass]
    public class NovelsControllerTests
    {
        // db var at class level for use in all tests
        private ApplicationDbContext context;
        NovelsController controller;

        // set up code that runs automatically before each unit test
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            context = new ApplicationDbContext(options);

            // add some data to the in-memory database
            context.Add(new Novel { NovelId = 1, Name = "Test Novel 1", Description = "Test Description 1", Photo = "Test Photo 1", MainTags = "Test MainTags 1" });
            context.Add(new Novel { NovelId = 2, Name = "Test Novel 2", Description = "Test Description 2", Photo = "Test Photo 2", MainTags = "Test MainTags 2" });
            context.Add(new Novel { NovelId = 3, Name = "Test Novel 3", Description = "Test Description 3", Photo = "Test Photo 3", MainTags = "Test MainTags 3" });
            context.SaveChanges();

            controller = new NovelsController(context);

        }
        #region index
        [TestMethod]
        public void DetailsValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Details(1).Result;

            // assert 
            Assert.AreEqual("Details", result.ViewName);
        }
        [TestMethod]
        public void IndexLoadsView()
        {

            var result = (ViewResult)controller.Index().Result;

            Assert.AreEqual("Index", result.ViewName);
        }
       
        [TestMethod]
        public void IndexLoadsNovels()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<Novel> model = (List<Novel>)result.Model;

            // assert
            CollectionAssert.AreEqual(context.Novel.OrderBy(p => p.Name).ToList(), model);
        }
        #endregion
        #region details
        [TestMethod]
        public void DetailsNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsNoNovelTableLoads404()
        {
            // arrange
            context.Novel = null;

            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(6).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DetailsValidIdLoadsNovel()
        {
            // act
            var result = (ViewResult)controller.Details(1).Result;

            // assert 
            Assert.AreEqual(context.Novel.Find(1), result.Model);
        }

        #endregion
        #region create
        [TestMethod]
        public void CreateValidNovel()
        {

            //create novel
            var novel4 = new Novel { NovelId = 4, Name = "Test Novel 4", Description = "Test Description 4", MainTags = "Test MainTags 4" };
            var stream = new MemoryStream();
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", "fake.jpg");
            //act
            controller.ModelState.AddModelError("", "No error");
            var result = (ViewResult)controller.Create(novel4, file).Result;

            //assert
            Assert.AreEqual(novel4, result.Model);
        }
        #endregion
        #region edit
        [TestMethod]
        public void EditNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditNoNovelLoads404()
        {
            // arrange
            context.Novel = null;
            
            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(6).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void EditValidIdLoadsNovel()
        {
            // act
            var result = (ViewResult)controller.Edit(1).Result;

            // assert 
            Assert.AreEqual(context.Novel.Find(1), result.Model);
        }
        [TestMethod]
        public void EditValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Edit(1).Result;

            // assert 
            Assert.AreEqual("edit", result.ViewName);
        }
        [TestMethod]
        public void EditValidNovel()
        {

            //create novel
            var novel4 = new Novel { NovelId = 4, Name = "Test Novel 4", Description = "Test Description 4", Photo = "photo4" ,MainTags = "Test MainTags 4" };
            var stream = new MemoryStream();
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", "fake.jpg");
            //act
            controller.ModelState.AddModelError("", "No error");
            var result = (ViewResult)controller.Edit(4 ,novel4, file, "oldfile").Result;

            //assert
            Assert.AreEqual(novel4, result.Model);
        }
        [TestMethod]
        public void EditInvalidValidNovel()
        {

            //create novel
            var novel4 = new Novel { NovelId = 4, Name = "Test Novel 4", Description = "Test Description 4", Photo = "photo4", MainTags = "Test MainTags 4" };
            var stream = new MemoryStream();
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", "fake.jpg");
            //act
            controller.ModelState.AddModelError("", "No error");
            var result = (ViewResult)controller.Edit(7, novel4, file, "oldfile").Result;

            //assert
            Assert.AreEqual("404", result.ViewName);
        }
        #endregion
        #region delete
        [TestMethod]
        public void DeleteNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteNoNovelTableLoads404()
        {
            // arrange
            context.Novel = null;

            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(6).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DeleteValidIdLoadsNovels()
        {
            // act
            var result = (ViewResult)controller.Delete(1).Result;

            // assert 
            Assert.AreEqual(context.Novel.Find(1), result.Model);
        }
        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Delete(1).Result;

            // assert 
            Assert.AreEqual("Delete", result.ViewName);
        }
        #endregion
        #region delete confirmed
        [TestMethod]
        public void DeleteConfirmedLoads()
        {
            var result = (RedirectToActionResult)controller.DeleteConfirmed(1).Result;

            // assert 
            Assert.AreEqual("Index", result.ActionName);
        }
        [TestMethod]
        public void DeleteConfirmedDoesntExist()
        {
            var result = (RedirectToActionResult)controller.DeleteConfirmed(7).Result;

            // assert 
            Assert.AreEqual("Index", result.ActionName);
        }
        #endregion
        #region novelExists

        [TestMethod]
        public void NovelExistsValidId()
        {
            // act
            var result = controller.NovelExists(1);

            // assert 
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void NovelExistsInvalidId()
        {
            // act
            var result = controller.NovelExists(6);

            // assert 
            Assert.AreEqual(false, result);
        }
        #endregion

    }

}