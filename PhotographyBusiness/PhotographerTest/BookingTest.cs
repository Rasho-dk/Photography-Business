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
        /// <summary>
        /// Ved Initialisere _bookingService har ma oprettet en instans af "_bookingService-klasse".
        /// Ved at oprette en instans af _bookingService-klasse, betyder at man kan bruge klassens metoder, der ikke statiske
        /// I _bookingService-Klasse har vi en konstrutør, der initializere en list af Booking fra Mockdata.
        /// Mock data bliver brugt til hver enkelt metod uafhængige af andre metoder. 
        /// </summary>

        [TestInitialize]
        public void Initialize()
        {   //Arannge
            _bookingService = new BookingService();
        }

        [TestMethod]
        public void TestGetAllBookings()
        {
            //Act
            List<Booking> bookings = _bookingService.GetAllBookings();
            //Assert
            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.Count() > 0);

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
        /// <summary>
        /// Kalder  på existerende booking objekt ved hjælper af metod Get AllBooking()[ vælger den en af dem].
        /// objektet er gemt i variablen.Ved hjælper af variable kan man vælge property og indtaster en ny værdi.
        /// Updatere man booking 
        /// tjekker om indholdet passer
        /// </summary>
        [TestMethod]
        public void Test_Updata_BookingInformation()
        {
            //Arrange
            var existingBooking = _bookingService.GetAllBookings()[1];
            existingBooking.Address = "Her is test address";
            existingBooking.AdminNote = "Hello, This is a test for update";
            existingBooking.DateTimeOfEvent = DateTime.Now;
            existingBooking.Price = 10;
            existingBooking.Category = "ME";

            //Act
            _bookingService.UpdateBooking(existingBooking);
            var updateBooking = _bookingService.GetBookingById(existingBooking.BookingId);

            //Assert
            Assert.IsNotNull(updateBooking);
            Assert.AreEqual(existingBooking.Address, updateBooking.Address);
            Assert.AreEqual(existingBooking.AdminNote, updateBooking.AdminNote);
            Assert.AreEqual(existingBooking.DateTimeOfEvent, updateBooking.DateTimeOfEvent);
            Assert.AreEqual(existingBooking.Price, updateBooking.Price);
            Assert.AreEqual(existingBooking.Category, updateBooking.Category);

        }
      

    }
}
