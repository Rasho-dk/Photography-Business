using Microsoft.AspNetCore.Identity;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockUsers
    {
        private static PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>()
        {
            new User(1,"admin@outlook.com", _passwordHasher.HashPassword(null, "admin"), "admin", "admin1231"),
            new User(4,"RashoRasho@hotmail.com", _passwordHasher.HashPassword(null, "123"), "Rasho Rash", "42424242"),
            new User(3,"Silas@outlook.com", _passwordHasher.HashPassword(null, "tyler1"), "Silas Silas", "42791451"),
            new User(6,"Sil@outlopk.com", _passwordHasher.HashPassword(null, "tyler1"), "Sil sil", "42791451")

        };

        public static List<User> GetMockUsers()
        {
            return users;
        }
    }
}
