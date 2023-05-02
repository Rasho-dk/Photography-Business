using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyBusiness.Models
{
    public class User
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsAdmin { get; set; }


        public User(string email, string password, string name, bool isAdmin, string phoneNumber)
        {
            Email = email;
            Password = password;
            Name = name;
            IsAdmin = isAdmin;
            PhoneNumber = phoneNumber;
        }

        public User()
        {

        }
    }
}
