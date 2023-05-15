using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services;
using PhotographyBusiness.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographerTest
{
    [TestClass]
    public class UserServiceTest
    {
        public IUserService ServiceUser { get; set; }
        public User? User { get; set; } 
        //public List<User> Users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            User = new User(1, "test@outlook.com", "test", "test123", "312312");
            ServiceUser = new UserService(); 
            //Users = new List<User>();

            //Act
            ServiceUser.CreateUserAsync(User);
        }
        [TestMethod]
        public async Task TestCreateUser()
        {
            //Arrange
             User = new User(1,"test@outlook.com", "test123", "test", "312312");

            //Act & Assert
             _ = ServiceUser.CreateUserAsync(User);       
           
            //Assert
            Assert.AreEqual("test",User.Name);
        }
        [TestMethod]
        public async Task Test_DeleteUserSuccessfully_ValidUserId()
        {
            //Arrange : Opretter en User Obj i Initialize metode
            
            //Act : User er blevet dannet i INitialize metode
            await ServiceUser.DeleteUserAsync(User.UserId);
            //Tjekke om der er user ind i Users(list of User) 
            User deletedUserFromListOfUser = await ServiceUser.GetUserByIdAsync(User.UserId);

            //Assert
            Assert.IsNull(deletedUserFromListOfUser);
        }
        [TestMethod]
        public async Task TestDeleteUserWithUInvaildUserId()
        {
            //Arrange
            User = null;
            int Invalid = 99;

            //Act : User er blevet dannet i INitialize metode
            var result = await ServiceUser.DeleteUserAsync(Invalid);

            //Assert
            Assert.IsNull(result);           
        }
        [TestMethod]
        public async Task TestGetUserByEmail()
        {
            //Arrange
            User = new User(88,"test@outlook.com","test123", "test", "312312");

            //Act
            _ = ServiceUser.CreateUserAsync(User);
            var result = await ServiceUser.GetUserByEmailAsync(User.Email);
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task TestGetUserByInvalidEamil()
        {
            //Arrange
            string InvaildEmail = "Invalid@hotmail.dk";

            //Act
            var result = await ServiceUser.GetUserByEmailAsync(InvaildEmail); 
            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task TestGetUserByName()
        {
            //Arrange
            User.Name = "test";

            //Act
            //_ = ServiceUser.CreateUserAsyn(User);           
            var result = await ServiceUser.GetUserByNameAsync(User.Name);

            //Assert
            Assert.AreEqual("test", result.Name); 
        }
        [TestMethod]
        public async Task TestGetUserNameInvalid()
        {
            //Arrange
            var InvaildName = "Invalid Name";

            //Act
           _ = ServiceUser.CreateUserAsync(User);
            var result = await ServiceUser.GetUserByNameAsync(InvaildName);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task TestGetUsetById_ValidId()
        {
            //Act
            var result = await ServiceUser.GetUserByIdAsync(User.UserId);
            //Assert
              Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task TestUpdateUserInfo()
        {
            //Arrange
            var email = "Change";
            var tel = "1234566787";

            //Act
           await ServiceUser.UpdateUserAsync(User);
        }
        [TestMethod]
        public async Task TestUpdateUserIfo_UserIsNull()
        {
            //Arrange 
            User = null;

            //Act
            var result = await ServiceUser.UpdateUserAsync(User);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void TestGetAllUser()
        {
            ServiceUser.GetAllUsers();
        }
    }
}
