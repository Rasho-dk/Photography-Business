using Microsoft.AspNetCore.Identity;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockUsers
    {
        private static PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>()
        {
            new User("admin@admin.com", _passwordHasher.HashPassword(null, "admin"), "admin", "22256142"),
            new User("123@123.com", _passwordHasher.HashPassword(null, "123"), "123", "42424242"),
            new User("tyler1@tyler1.com", _passwordHasher.HashPassword(null, "tyler1"), "tyler1", "42791451")
        };

        public static List<User> GetMockUsers()
        {
            return users; 
        }
    }
}
