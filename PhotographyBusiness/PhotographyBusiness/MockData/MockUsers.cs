using Microsoft.AspNetCore.Identity;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockUsers
    {
        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>()
        {
            new User("admin", passwordHasher.HashPassword(null, "admin"), "admin", true),
            new User("123", passwordHasher.HashPassword(null, "123"), "123", false),
            new User("tyler1", passwordHasher.HashPassword(null, "tyler1"), "tyler1", false)
        };

        public static List<User> GetMockUsers()
        { 
            return users; 
        }
    }
}
