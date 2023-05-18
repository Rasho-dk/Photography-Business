﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PhotographyBusiness.Models
{
    public class Booking
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        [Required]
        public string Category { get; set; }
        [DataType(DataType.Currency), AllowNull]
        public double? Price { get; set; }
        [Required]
        public string CustomerNote { get; set; }
        [AllowNull]
        public string? AdminNote { get; set; }
        [Required, DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "16/05/2023", "31/12/2099", ErrorMessage = "Date has to be after todays date")]
        public DateTime Date { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        public bool IsAccepted { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int UserId { get; set; }

        [NotMapped]
        public User User { get; set; }

        /// <summary>
        /// The full constructor
        /// </summary>
        /// <param name="bookingId">The id of the booking</param>
        /// <param name="category">Required for the photographer to know what kind of photography the job belongs to</param>
        /// <param name="price">Not required, will be discussed with emails by photographer</param>
        /// <param name="customerNote">The customer is required to write some text about the photo job</param>
        /// <param name="adminNote">The admin can take notes to remember and take note of certain things</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo">The date to, if the event stretches over many days</param>
        /// <param name="isAccepted">Returns false if the admin has not accepted the booking, and true if he has</param>
        public Booking(string category, double price, string customerNote, string adminNote, DateTime date, DateTime dateCreated, bool isAccepted, string address, User user, int userId)
        {
            Category = category;
            Price = price;
            CustomerNote = customerNote;
            AdminNote = adminNote;
            Date = date;
            DateCreated = dateCreated;
            IsAccepted = isAccepted;
            Address = address;
            User = user;
            //UserId = userId;
        }

        /// <summary>
        /// Booking Request Constructor, only used to instantiate the request. Therefore IsAccepted = false.
        /// These are the parameters that are required in order to create a booking request.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="customerNote"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        public Booking(string category, string customerNote, string address, User user, DateTime date)
        {
            User = user;
            Category = category;
            CustomerNote = customerNote;
            Date = date;
            DateCreated = DateTime.Now;
            Address = address;
            IsAccepted = false;
        }

        /// <summary>
        /// The default constructor used for DB
        /// </summary>
        public Booking() {  }

    }
}
