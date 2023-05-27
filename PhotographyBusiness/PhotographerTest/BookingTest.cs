using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographerTest
{
    [TestClass]
    public class BookingTest
    {
        private IBookingService _bookingService;
        public IUserService _userService { get; set; }
       
        [TestInitialize]
        public void Initialize()
        {
            _bookingService = new BookingService();
            _userService = new UserService();
        }
        [TestMethod]
        public void TestGetAllBookings()
        {
            _bookingService.GetAllBookings();
        }

        [TestMethod]
        public void Test_CreateBooking()
        {
            //Arannge
            Booking newBooking = new Booking("Party", "pls take pics", "Mrs Bo 300 Cherry Court SOUTHAMPTON SO53 5PD UK",
                   new User("Bo@email.com", "123", "Bo Bo", "12345678"),
                   new DateTime(2023, 10, 10, 12, 30, 00))
            { BookingId = 123 };
            newBooking.IsAccepted = true;
            var actual_firstCount = _bookingService.GetAllBookings().Count;

            //Act
            _bookingService.CreateBookingAsync(newBooking);
            var expected_SecCount = _bookingService.GetAllBookings().Count;

            //Assert
            Assert.AreEqual(expected_SecCount, ++actual_firstCount);

        }
        [TestMethod]
        public void Test_CreateBooking_InvalidInfor()
        {
            Booking booking = null;

            Assert.ThrowsExceptionAsync<Exception>(() => _bookingService.CreateBookingAsync(booking));
        }
        [TestMethod]
        public void Test_DeleteBookingById_ValidId()
        {
            //Arrange
            var actual_fistCount = _bookingService.GetAllBookings().Count();

            var BookingId = _bookingService.GetAllBookings()[0];

            //Act
            var result = _bookingService.DeleteBooking(BookingId.BookingId);

            var expected = _bookingService.GetAllBookings().Count;

            //Assert
            Assert.AreNotEqual(expected, actual_fistCount);
        }
        [TestMethod]
        public void Test_DeleteBookingById_InValidId()
        {
            //Arrange
            int invalidBookingId = 012;

            //Act
            var result = _bookingService.DeleteBooking(invalidBookingId);
            var resultOFDeleteBooking = _bookingService.GetBookingById(invalidBookingId);

            //Assert
            Assert.IsNull(resultOFDeleteBooking);
        }
        [TestMethod]
        public void GetBookingByUserId_BookingId()
        {
            //Arrange
            Booking booking = new Booking();
            booking.User = new User();
            booking.User.UserId = 99;
            booking.UserId = booking.User.UserId;
            booking.BookingId = 99;

            //Act
            _bookingService.CreateBookingAsync(booking);
            var result = _bookingService.GetBookingById_User(99, 99);

            //Assert
            Assert.AreEqual(result.BookingId, booking.BookingId);
            Assert.AreEqual(result.UserId, booking.UserId);
        }
        [TestMethod]
        public void GetBookingByUserId_BookingId_InvalindId()
        {
            //Arrange
            int userId = 22;
            int bookingId = 22;

            //Act
            var result = _bookingService.GetBookingById_User(userId, bookingId);

            //Asset
            Assert.IsNull(result); 
        }
        [TestMethod]
        public void Test_GetBookingsByUserId_Valind()
        {
            //Arrange
            Booking booking   = new Booking { User = new User() };
            booking.User.UserId = 99;

            //Act
            _bookingService.CreateBookingAsync(booking);
            var result = _bookingService.GetBookingsByUserId(99);

            //Asset
            Assert.IsTrue(result.Contains(booking));
        }
        /// <summary>
        /// Formålet er at kalde på existerende booking objekt(obj) ved at bruge GettAllBooking()[index].
        ///indtaster en ny værdier ved hjælp af varibelen. 
        ///Tjekker om inholdet passer
        /// </summary>
        [TestMethod]
        public void Test_Update_BookingInforamtion()
        {
            //Arrange
            var existingBooking = _bookingService.GetAllBookings()[1];
            existingBooking.Address = "En ny address";
            existingBooking.AdminNote = "Husk have en kaffe med";
            existingBooking.Date = DateTime.Now;
            existingBooking.Price= 100;
            existingBooking.Category = "Fest";

            //Act
            _bookingService.UpdateBooking(existingBooking);
            var updatedBooking = _bookingService.GetBookingById(existingBooking.BookingId);

            //Assert
            Assert.IsNotNull(updatedBooking);
            Assert.AreEqual(existingBooking, updatedBooking);

        }


    }
}
