using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByNameAsync(string name);
        Task<User> DeleteUserAsync(int id);
        Task CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<List<User>> Filtering(string filter);  

    }
}
