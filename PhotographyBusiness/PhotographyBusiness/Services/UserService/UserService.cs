using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.UserService
{
    public class UserService : IUserService
    {
        private List<User> users; 
        private GenericDbService<User> _genericDbService; 
        private MailService.IMailService _mailService;
        public UserService(GenericDbService<User> genericDbService, MailService.IMailService mailService)
        {
            _genericDbService = genericDbService;
            _mailService = mailService;
            users = genericDbService.GetObjectsAsync().Result.ToList();
            //users = MockData.MockUsers.GetMockUsers();
        }
        /// <summary>
        /// konsturtøren bliver brugt til unit test.
        /// MockData bliver brug ift. CRUD unittest. 
        /// </summary>
        public UserService()
        {
            users = MockData.MockUsers.GetMockUsers();
        }

        public async Task CreateUserAsync(User user)
        {
            users.Add(user);    
            await _genericDbService.AddObjectAsync(user);
            await _mailService.SendUserCreationEmail(user.Email, user.Name);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            User userToBeDeleted = null;
            foreach(var user in users)
            {
                if(user.UserId.Equals(id))
                {
                    userToBeDeleted = user;
                    break;
                }
            }
            if(userToBeDeleted != null)
            {
                users.Remove(userToBeDeleted);
                await _genericDbService.DeleteObjectAsync(userToBeDeleted); 
            }
            return userToBeDeleted;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            foreach(var user in users)
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
            foreach(var user in users)
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
            return users;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            foreach(var user in users)
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
                foreach (var us in users)
                {
                    if (us.UserId.Equals(user.UserId))
                    {
                        us.Email = user.Email;
                        us.PhoneNumber = user.PhoneNumber;
                    }
                }
                await _genericDbService.UpdateObjectAsync(user);
            }
            return null;
        }

        public async Task<List<User>> Filtering(string filter)
        {
           var filterData = string.IsNullOrEmpty(filter) ? users : users
                .Where(data => data.Name.ToLower().Contains(filter.ToLower()) ||
                data.PhoneNumber.Contains(filter)).ToList();
            return filterData;
        }
    }
}
