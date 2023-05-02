using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.UserService
{
    public class UserSerice : IUserService
    {
        private List<User> users;
        private GenericDbService<User> _grDbUserService;
        public UserSerice(GenericDbService<User> genericDbService)
        {
            _grDbUserService = genericDbService;
            //Users = genericDbService.GetObjectsAsync().Result.ToList(); 
        }
        public UserSerice()
        {
            users = MockData.MockUsers.GetMockUsers();
        }
        public async Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            return users;

        }

        public async Task<User> GetStudentById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
