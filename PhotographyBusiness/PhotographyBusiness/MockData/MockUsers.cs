using Microsoft.AspNetCore.Identity;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockUsers
    {
        private static PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>()
        {
            new User(1,"admin@admin.com", _passwordHasher.HashPassword(null, "admin"), "admin", "22256142"),
            new User(2,"123@123.com", _passwordHasher.HashPassword(null, "123"), "sheroRasho", "42424242"),
            new User(3,"tyler1@tyler1.com", _passwordHasher.HashPassword(null, "tyler1"), "tyler1", "42791451")
        };

        public static List<User> GetMockUsers()
        {
            return users; 
        }
    }
}
