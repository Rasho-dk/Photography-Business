using Castle.Core.Smtp;
using Moq;
using PhotographyBusiness.Pages.AccountPages;
using PhotographyBusiness.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographerTest
{
    [TestClass]
    public class TestLogIn
    {
        private IUserService _userService;  
        private LogInPageModel _logInPageModel;
        [TestMethod]
        public void Login_Successfully()
        {
            var userServiceMock = new Mock<IUserService>();
            



        }

    }
}
