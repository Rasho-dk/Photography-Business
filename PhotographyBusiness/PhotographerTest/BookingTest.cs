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
        public async Task Test_GetBookingById_Valid()
        {

        }
        [TestMethod]
        public void Test_GetBookingById_User_ValindUserId()
        {
            //Arrange
            int userId = 4;

            //Act

            //Asset
        }
        [TestMethod]
        public void Test_GetBookingById_User_InvalidUserId()
        {
            //Arrange

            //Act


            //Asset
        }

    }
}
