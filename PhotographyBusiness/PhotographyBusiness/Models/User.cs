using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
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

        [Required(ErrorMessage = "Please enter your email address.")]
        [StringLength(40, ErrorMessage = "The email address must be no more than {1} characters long.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address. exampel@exampel.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "The Name must be between 5 and 12 characters long.")]
        public string Name { get; set; }

        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public DateTime DateCreated { get; set; }

        [NotMapped]
        [BindNever]
        public virtual ICollection<Booking> Bookings { get; set; }
 
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
            DateCreated = DateTime.Now;
        }

        public User(int id ,string email, string password, string name, string phoneNumber)
        {
            UserId = id ;
            Email = email;
            Password = password;
            Name = name;
            PhoneNumber = phoneNumber;
            DateCreated = DateTime.Now;
        }
        /// <summary>
        /// Defeault constructor for DB
        /// </summary>
        public User()
        {
     
        }
    }
}
