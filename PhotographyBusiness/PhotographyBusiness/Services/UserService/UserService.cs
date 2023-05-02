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
            User userToBeDeleted = null;
            foreach(var user in _users)
            {
                if(user.UserId.Equals(id))
                {
                    userToBeDeleted = user;
                }
            }
            if(userToBeDeleted != null)
            {
                _users.Remove(userToBeDeleted);
                await _genericDbService.DeleteObjectAsync(userToBeDeleted); 
            }
            return userToBeDeleted;
        }

        public List<User> GetAllUsers()
        {
            return _users;

        }

        public async Task<User> GetUserById(int id)
        {
            foreach(var user in _users)
            {
                if(user.UserId.Equals(id))
                    return user;
            }
            return null;
        }

        public async Task UpdateUser(User user)
        {
            if(user is not null)
            {
                foreach(var us in _users)
                {
                    if (us.UserId.Equals(user.UserId))
                    {
                        //TODO Update User
                    }
                }
                await _genericDbService.UpdateObjectAsync(user);
            }
        }
    }
}
