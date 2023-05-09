﻿using Microsoft.AspNetCore.Mvc;
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
        [StringLength(100, ErrorMessage = "The email address must be no more than {1} characters long.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address. exampel@exampel.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 20 characters long.")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        
        public DateTime DateCreated { get; set; }

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
        /// <summary>
        /// Defeault constructor for DB
        /// </summary>
        public User()
        {
     
        }
    }
}
