using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        Task<User> GetUserByIdAsyn(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> DeleteUserAsyn(int id); 
        Task CreateUserAsyn(User user); 
        Task UpdateUserAsyn(User user); 

    }
}
