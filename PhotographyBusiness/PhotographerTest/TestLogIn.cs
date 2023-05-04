using Moq;
using PhotographyBusiness.Pages.AccountPages;
using PhotographyBusiness.Services.UserService;

namespace PhotographerTest
{
    [TestClass]
    public class TestLogIn
    {
        private IUserService _userService;
        [TestMethod]
        public void Login_Test_HappyPath()
        {
            var userservicemock = new Mock<IUserService>();
            var model = new LogInPageModel(userservicemock.Object)
            {
                Email = "Test@Test.com",
                Password = "12345678"
            };
            Assert.AreEqual("12345678", model.Password);
            Assert.AreEqual("Test@Test.com", model.Email);

        }
        [TestMethod]
        public void Login_UnHappyPath()
        {
            var userservicemock = new Mock<IUserService>();
            var model = new LogInPageModel(userservicemock.Object)
            {
                Email = "Test@Test.com",
                Password = "12345678"
            };
            try
            {

            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Error, invalid email or password.Please try again",ex.Message);
            }

        }

    }
}
