using Microsoft.AspNetCore.Mvc;
using PhotographyBusiness.Pages.AccountPages;
using PhotographyBusiness.Services.UserService;
using Moq;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.MailService;

namespace PhotographerTest
{
    [TestClass]
    public class CreateUserPageModelTest
    {
        [TestMethod]
        public void CreateUserPageModel_OnPost_UnSuccessfullyCreatesNewUser()
        {
            //Her bliver oprettet en mock obj af "IUserService" interface fra "Mock" Klass som hedder Moq library.

            var userservicemock = new Mock<IUserService>();
            var mailservicemock = new Mock<IMailService>();

            //Arrange

            //Her bliver der oprettes en ny instance af CreateUserPageModel ved at passere "userservicemock" som en 
            //konstrutur som bruger Object property af userservicemock.
            //Den giver lov til at oprette en instance af "CreateUserPageModel" med en fake implementation. 
            var model = new CreateUserPageModel(userservicemock.Object, mailservicemock.Object)
            {
                Email = "Test@hotmail.com",

                //Her kan se at de fejler at Password passer ikke med hinanden. 
                Password = "123456789",
                RepeatPassword = "12345678",
                FirstName = "SILAS",
                LastName = "Hello",
                PhoneNumber = "123456789012"
            };

            try
            {
                User CallBackUser = null;
                userservicemock.Setup(x => x.CreateUserAsync(It.IsAny<User>())).Throws(new Exception("User cant be created"));
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
            var mailservicemock = new Mock<IMailService>();

            //Arrange

            //Her bliver der oprettes en ny instance af CreateUserPageModel ved at passere "userservicemock" som en 
            //konstrutur som bruger Object property af userservicemock.
            //Den giver lov til at oprette en instance af "CreateUserPageModel" med en fake implementation. 
            var model = new CreateUserPageModel(userservicemock.Object, mailservicemock.Object)
            {
                Email = "Test@hotmail.com",
                PhoneNumber = "123456789012",
                Password = "123456789",
                RepeatPassword = "123456789",
                FirstName = "SILAS",
                LastName ="Hello"
            };

            User CallBackUser = null;
            userservicemock
                .Setup(x => x.CreateUserAsync(It.IsAny<User>()))
                .Callback<User>(u => CallBackUser = u);

            //Act 
            var result = model.OnPost();

            Xunit.Assert.IsType<RedirectToPageResult>(result);
            Xunit.Assert.NotNull(CallBackUser);
            Xunit.Assert.Equal(model.Email, CallBackUser.Email);
            Xunit.Assert.Equal(model.FirstName + " " + model.LastName, CallBackUser.Name);
            Xunit.Assert.Equal(model.PhoneNumber, CallBackUser.PhoneNumber);
            Xunit.Assert.Equal(model.Password, CallBackUser.Password);

        }
       

    }

}

