using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyBusiness.Models
{
    public class User
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

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

        /// <summary>
        /// The full constructor
        /// </summary>
        /// <param name="email">Email of the user. Used to log in</param>
        /// <param name="password">Hashed password of the user.</param>
        /// <param name="name">Name of the user</param>
        /// <param name="phoneNumber">Phonenumber of the user</param>

        public User(string email, string password, string name, string phoneNumber)
        {
            Email = email;
            Password = password;
            Name = name;
            PhoneNumber = phoneNumber;
        }
        /// <summary>
        /// Defeault constructor for DB
        /// </summary>
        public User()
        {
     
        }
    }
}
