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
            //_genericDbService.SaveObjects(Users);

        }
        //Shero: This for Unittest
        public UserService()
        {
            Users = new List<User>();
        }

        public async Task CreateUserAsync(User user)
        {
            Users.Add(user);    
           // await _genericDbService.AddObjectAsync(user);
        }

        public async Task<User> DeleteUserAsync(int id)
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
                //await _genericDbService.DeleteObjectAsync(userToBeDeleted); 
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
                    return  user;
                }
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            return Users; 

        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            foreach(var user in Users)
            {
                if(user.UserId.Equals(id))
                    return user;
            }
            return null;
        }

        public async Task<User> UpdateUserAsync(User user)
        {          
            if (user is not null)
            {
                foreach (var us in Users)
                {
                    if (us.UserId.Equals(user.UserId))
                    {
                        us.Email = user.Email;
                        us.PhoneNumber = user.PhoneNumber;
                    }
                }
                // await _genericDbService.UpdateObjectAsync(user);
            }
            return null;
        }
    }
}
