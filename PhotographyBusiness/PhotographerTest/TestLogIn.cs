﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhotographyBusiness.Models;
using PhotographyBusiness.Pages.AccountPages;
using PhotographyBusiness.Services.UserService;

namespace PhotographerTest
{
    [TestClass]
    public class TestLogIn
    {
        private IUserService _userService;
        //[TestMethod]
        //public void Login_Test_HappyPath_Retur_False()
        //{
        //    var userservicemock = new Mock<IUserService>();
        //    var model = new LogInPageModel(userservicemock.Object)
        //    {
        //        Email = "Test@outlook.com",
        //        Password = "123456789"
        //    };
        //    var users = new List<User>()
        //    {
        //        new User() {Name = "Test",Password="123456789",Email="Test@outlook.com"},
        //        //new User(){Name = "admin",Password ="admin123",Email="admin@admin.dk"}
        //    };

        //    userservicemock.Setup(x => x.GetAllUsers()).Returns(users);
        //    var result = model.Login(model.Email, model.Password);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(true, result);
        //}
        //[TestMethod]
        //public void Login_Test_UnHappyPath_Return_True()
        //{
        //    var userservicemock = new Mock<IUserService>();
        //    var model = new LogInPageModel(userservicemock.Object)
        //    {
        //        Email = "Test@outlook.com",
        //        Password = "123456"
        //    };
        //    var users = new List<User>()
        //    {
        //        new User() {Name = "Test",Password="123456789",Email="Test@outlook.com"},
        //        //new User(){Name = "admin",Password ="admin123",Email="admin@admin.dk"}
        //    };

        //    userservicemock.Setup(x => x.GetAllUsers()).Returns(users);
        //    var result = model.Login(model.Email, model.Password);
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(false, result);
        //}
        private static PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        [TestMethod]
        public async Task Login_OnPostTest_HappyPath_()
        {

            //Arrange
            var userservicemock = new Mock<IUserService>();
            var model = new LogInPageModel(userservicemock.Object)
            {             
                Email = "Test@outlook.com",
                Password = "123456789",

            };

            var users = new List<User>()
            {
            new User()
            { Name = "Test",
             Password = "123456789",
              Email = "Test@outlook.com"
            }
            };

            //Act
            #region
            //Setup : metod fra Moq biblioteket. Oprete en Mock/falsk implementation af "IUserService" og bruger GetAllUsers fra IUserService og definerer list af brugere. 
            //GetAllUsers kladers i "LogIndPage" klass som returner list af brugeren som er difineret i unittestet i sted for at kontakte dataBase.. Det gør muligt at test uafhængig af DB
            #endregion
            userservicemock.Setup(x => x.GetAllUsers()).Returns(users);
            var result = await model.OnPostAsync();

            //Assert 
            //Check om result er ikke null
            Assert.IsNotNull(result);
            #region
            //Da "result" returner "IActionResult" instance, men i unit test kraves en specific type af result, hvilken er "RedirectToPageResult"..
            //Derfor bruger vi type cast "(RedirectToPageResult)" til convertere "IActionResult" instance til RedirectToPageResult ved at bruge "(RedirectToPageResult)result)"
            //PageName er em property af RedirectToPageResult  bruger til at give navn på Razor Page som bruger navigere til
            #endregion
            Assert.AreEqual("/Index", ((RedirectToPageResult)result).PageName);
        }
        [TestMethod]
        public void Login_OnPostTest_UnHappyPath_ForBoth_Password_And_Email()
        {
            var userservicemock = new Mock<IUserService>();
            var model = new LogInPageModel(userservicemock.Object)
            {
                Email = "Test1@Test1.com",
                Password = "123456789"
            };

            var users = new List<User>()
            {
                new User() {Name = "Test1",Password="1234567890",Email="Test1@Test1.com"},

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