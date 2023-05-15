using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographerTest
{
    [TestClass]
    public class BookingTest
    {
        public IBookingService BookingService { get; set; }
        public IUserService UserService { get; set; }
        public User User { get; set; }
        public Booking Booking { get; set; }
        [TestInitialize] 
        public void Initialize() 
        {
            Booking = new Booking("Marriage", 100, "Please give me photos :3", "Yaaaah :3",
                new DateTime(2023, 03, 12, 14, 15, 00), new DateTime(2023, 03, 13), true,
                "Mrs Rasho 71 Cherry Court SOUTHAMPTON SO53 5PD UK",
                User = new User(4, "RashoRasho@hotmail.com", "123", "Rasho Rash", "Rasho123"), 1) { BookingId = 1 };

            BookingService = new BookingService();

            UserService = new UserService();    
            User = new User();

        }
        [TestMethod]
        public async Task Test_CreateBooking()
        {
             await BookingService.CreateBookingAsync(Booking);
        }
        [TestMethod]
        public async Task Test_createBooking_InvalidInfor()
        {
           Booking = null;
            Assert.ThrowsExceptionAsync<Exception>(() => BookingService.CreateBookingAsync(Booking));
        }
        [TestMethod]
        public async Task Test_BookingDelete()
        {
           await Test_CreateBooking();
            var validBookingId = BookingService.GetBookingById(Booking.BookingId);  
           await  BookingService.DeleteBooking(validBookingId.BookingId);
        }
        [TestMethod]
        public async Task Test_GetBookingById_Valid()
        {
            BookingService.GetBookingById(Booking.BookingId);
        }
        [TestMethod]
        public void TestGetAllBookings()
        {
            BookingService.GetAllBookings();
        }
    }
}
