
using Microsoft.AspNetCore.Mvc;
using PhotographyBusiness.Pages.UserPages;

namespace PhotographerTest
{
    [TestClass]
    public class CreateUserPageModelTest
    {
        [TestMethod]
        public void TestCreateUserWithValidData()
        {
            //Arrange
            var model = new CreateUserPageModel();
            model.Email = "test@test.dk";
            model.Password = "test123";
            model.PhoneNumber = "12334";
            model.FullName = "BO Ali";

            //Result
            var result = model.OnPost();

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            Assert.AreEqual("Index",((RedirectToPageResult)result).PageName);


        }
    }
}