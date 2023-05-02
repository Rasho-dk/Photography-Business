using Microsoft.AspNetCore.Mvc;
using PhotographyBusiness.Pages.UserPages;
using PhotographyBusiness.Services.UserService;
using Moq;
using PhotographyBusiness.Models;
//using Xunit;


namespace PhotographerTest
{
    [TestClass]
    public class CreateUserPageModelTest
    {
        private IUserService _userService;
        private CreateUserPageModel _createUserPageModel;
        [TestInitialize]
        public void Initialze()
        {
            _userService = new UserService();
            _createUserPageModel = new CreateUserPageModel(_userService);
            User user = new User()
            {
                Email = "Test@homtail.com",
                Name = "Arun",
                PhoneNumber = "1234567890"
            };
        }

        [TestMethod]
        public void CreateUserPageModel_OnPost_UnSuccessfullyCreatesNewUser()
        {
            //Her bliver oprettet en mock obj af "IUserService" interface fra "Mock" Klass som hedder Moq library.

            var userservicemock = new Mock<IUserService>();

            //Arrange

            //Her bliver der oprettes en ny instance af CreateUserPageModel ved at passere "userservicemock" som en 
            //konstrutur som bruger Object property af userservicemock.
            //Den giver lov til at oprette en instance af "CreateUserPageModel" med en fake implementation. 
            var model = new CreateUserPageModel(userservicemock.Object)
            {
                Email = "Test@hotmail.com",

                //Her kan se at de fejler at Password passer ikke med hinanden. 
                Password = "123456789",
                ConfirmPassword = "12345678",

                FullName = "SILAS Hello",
                PhoneNumber = "123456789012"
            };

            try
            {
                User CallBackUser = null;
                userservicemock.Setup(x => x.CreateUser(It.IsAny<User>())).Throws(new Exception("User cant be created"));
                _createUserPageModel.OnPost();
            }
            catch (Exception ex)
            {
                Xunit.Assert.Equal("User cant be created", ex.Message);
            }


        }

        [TestMethod]
        public void CreateUserPageModel_OnPost_SuccessfullyCreatesNewUser()
        {
            //Her bliver oprettet en mock obj af"IUserService" interface fra "Mock" Klass som hedder Moq library.
            var userservicemock = new Mock<IUserService>();

            //Arrange

            //Her bliver der oprettes en ny instance af CreateUserPageModel ved at passere "userservicemock" som en 
            //konstrutur som bruger Object property af userservicemock.
            //Den giver lov til at oprette en instance af "CreateUserPageModel" med en fake implementation. 
            var model = new CreateUserPageModel(userservicemock.Object)
            {
                Email = "Test@hotmail.com",
                Password = "123456789",
                ConfirmPassword = "123456789",
                FullName = "SILAS Hello",
                PhoneNumber = "123456789012"
            };

            User CallBackUser = null;
            userservicemock
                .Setup(x => x.CreateUser(It.IsAny<User>()))
                .Callback<User>(u => CallBackUser = u);
            //Act 
            var result = model.OnPost();

            Xunit.Assert.IsType<RedirectToPageResult>(result);
            Xunit.Assert.NotNull(CallBackUser);
            Xunit.Assert.Equal(model.Email, CallBackUser.Email);
            Xunit.Assert.Equal(model.FullName, CallBackUser.Name);
            Xunit.Assert.Equal(model.PhoneNumber, CallBackUser.PhoneNumber);
            Xunit.Assert.Equal(model.Password, CallBackUser.Password);

        }
       

    }

}

