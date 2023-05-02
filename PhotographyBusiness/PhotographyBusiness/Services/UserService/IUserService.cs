using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        Task<User> GetStudentById(int id);
        Task<User> DeleteUser(int id); 
        Task CreateUser(User user); 
        Task UpdateUser(User user); 

    }
}
