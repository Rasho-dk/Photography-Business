using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.UserService
{
    public class UserService : IUserService
    {
        public List<User> Users { get; set; } 
        private GenericDbService<User> _genericDbService;
        public UserService(GenericDbService<User> genericDbService)
        {
            _genericDbService = genericDbService;
            //Users = genericDbService.GetObjectsAsync().Result.ToList(); 
            Users = MockData.MockUsers.GetMockUsers();

        }
        public UserService()
        {
            
        }
        public async Task CreateUserAsyn(User user)
        {
            Users.Add(user);    
            await _genericDbService.AddObjectAsync(user);
        }

        public async Task<User> DeleteUserAsyn(int id)
        {
            User userToBeDeleted = null;
            foreach(var user in Users)
            {
                if(user.UserId.Equals(id))
                {
                    userToBeDeleted = user;
                }
            }
            if(userToBeDeleted != null)
            {
                Users.Remove(userToBeDeleted);
                await _genericDbService.DeleteObjectAsync(userToBeDeleted); 
            }
            return userToBeDeleted;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            foreach(var user in Users)
            {
                if (user.Email.Equals(email)) 
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            foreach(var user in Users)
            {
                if (user.Name.Equals(name))
                {
                    return user;
                }
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            return Users; 

        }
        public async Task<User> GetUserByIdAsyn(int id)
        {
            foreach(var user in Users)
            {
                if(user.UserId.Equals(id))
                    return user;
            }
            return null;
        }

        public async Task UpdateUserAsyn(User user)
        {
            if(user is not null)
            {
                foreach(var us in Users)
                {
                    if (us.UserId.Equals(user.UserId))
                    {
                        //TODO Update User

                    }
                }
                await _genericDbService.UpdateObjectAsync(user);
            }
        }
        public User GetUserBystr(string str) => GetAllUsers().Find(user => user.Name.ToLower() == str.ToLower());
    }
}
