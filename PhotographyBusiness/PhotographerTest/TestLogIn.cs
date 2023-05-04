using Moq;
using PhotographyBusiness.Models;
using PhotographyBusiness.Pages.AccountPages;
using PhotographyBusiness.Services.UserService;
using System.Security.Authentication;

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
        //[ExpectedException(typeof(FormatException))]
        public void Login_UnHappyPath()
        {
            var userservicemock = new Mock<IUserService>();
            var model = new LogInPageModel(userservicemock.Object)
            {
                Email = "Test@Test.com",
                Password = "1234567890"
            };

            var users = new List<User>()
            {
                new User() {Name = "Test",Password="123456789",Email="Test@Test.com"},
                //new User(){Name = "admin",Password ="admin123",Email="admin@admin.dk"}
            };

            try
            {
                userservicemock.Setup(x => x.GetAllUsers()).Returns(users);
                var ex = Assert.ThrowsException<Exception>(() => model.OnPostAsync());
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid email or password.Please try again", model.DisplayMessage);
            }







        }
    }
}
