using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.UserService
{
    public class UserService : IUserService
    {
        private List<User> _users;
        private GenericDbService<User> _genericDbService;
        public UserService(GenericDbService<User> genericDbService)
        {
            _genericDbService = genericDbService;
            //Users = genericDbService.GetObjectsAsync().Result.ToList(); 
        }
        public UserService()
        {
            _users = MockData.MockUsers.GetMockUsers();
        }
        public async Task CreateUser(User user)
        {
            _users.Add(user);    
            await _genericDbService.AddObjectAsync(user);
        }

        public async Task<User> DeleteUser(int id)
        {

        }

        public List<User> GetAllUsers()
        {
            return _users;

        }

        public async Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
