using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelWebApp.Controllers;
using NovelWebApp.Data;

namespace NovelWebAppTests
{
    [TestClass]
    public class NovelsControllerTests
    {
        [TestMethod]
        public void IndexLoadsView()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);

            var controller = new NovelsController(context);

            var result = (ViewResult)controller.Index().Result;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        
    }
}