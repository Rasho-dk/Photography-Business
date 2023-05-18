using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.BookingService
{
    public interface IBookingService
    {

        public List<Booking> GetAllBookings();
        public Booking GetBookingById(int id);
        public List<Booking> GetBookingsByUserId(int userId);
        public Task DeleteBooking(int id);
        public Task CreateBookingAsync(Booking booking);
        public Task UpdateBooking(Booking booking);
        public List<Booking> GetAllBookingsThisMonth();
        public List<Booking> GetUpcomingBookings();
        public List<Booking> GetMostRecentRequests();
        public List<Booking> GetAllBookingsRequests();
        Booking GetBookingById_User(int id);
        public Task<List<Booking>> FilterBookingsByDate(DateTime startdate, DateTime endDate);
        public Task<List<Booking>> FilterBookingsByNameOrEmail(string searchinput);
        public Task<List<Booking>> FilterBookingsByCategory(string Category);
        public Task<List<Booking>> SortBookingByCategory();
        public Task<List<Booking>> SortBookingByDate();
        public Task<List<Booking>> SortBookingByName();
        public Task<List<Booking>> SortBookingByEmail();
        List<Booking> GetBookingById_User_(int id);



    }
}
