using Microsoft.AspNetCore.Identity;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockUsers
    {
        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>()
        {
            new User("admin@admin.com", passwordHasher.HashPassword(null, "admin"), "admin", true, "22256142"),
            new User("123@123.com", passwordHasher.HashPassword(null, "123"), "123", false, "42424242"),
            new User("tyler1@tyler1.com", passwordHasher.HashPassword(null, "tyler1"), "tyler1", false, "42791451")
        };

        public static List<User> GetMockUsers()
        { 
            return users; 
        }
    }
}
