using PhotographyBusiness.Models;
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
        public UserService service { get; set; }
        public User user { get; set; } 

        [TestInitialize]
        public void Initialize()
        {
            var User = new User("Foo@hotmail.com","123","Foo","123456");

        }
        [TestMethod]
        public async Task Test_CreateUser()
        {
            var User = new User("Foo@hotmail.com", "123", "Foo", "123456");
            List<User> users = new List<User>();

            await service.CreateUserAsyn(user);
            //{
            //    users.Add(User);
            //};

        }
    }
}
