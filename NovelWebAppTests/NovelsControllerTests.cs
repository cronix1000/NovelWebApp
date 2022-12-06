using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelWebApp.Controllers;
using NovelWebApp.Data;
using NovelWebApp.Models;

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
            var context = new ApplicationDbContext(options);

            // add some data to the in-memory database
            context.Novel.Add(new Novel { NovelId = 1, Name = "Test Novel 1", Description = "Test Description 1", Photo = "Test Photo 1", Author = "Test Author 1", MainTags = "Test MainTags 1" });
            context.Novel.Add(new Novel { NovelId = 2, Name = "Test Novel 2", Description = "Test Description 2", Photo = "Test Photo 2", Author = "Test Author 2", MainTags = "Test MainTags 2" });
            context.Novel.Add(new Novel { NovelId = 3, Name = "Test Novel 3", Description = "Test Description 3", Photo = "Test Photo 3", Author = "Test Author 3", MainTags = "Test MainTags 3" });
            context.SaveChanges();

            controller = new NovelsController(context);

        }
        #region index

        [TestMethod]
        public void IndexLoadsView()
        {

            var result = (ViewResult)controller.Index().Result;

            Assert.AreEqual("Index", result.ViewName);
        }

        //dooesnt work fo now
        [TestMethod]
        public void IndexLoadsProducts()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<Novel> model = (List<Novel>)result.Model;

            // assert
            CollectionAssert.AreEqual(context.Novel.OrderBy(p => p.Name).ToList(), model);
        }

        #endregion
        #region details
        #endregion 
        #region create
        #endregion
        #region edit
        #endregion
        #region delete
        #endregion
        
    }
}