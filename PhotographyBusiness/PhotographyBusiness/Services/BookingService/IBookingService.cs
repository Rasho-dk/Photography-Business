using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.BookingService
{
    public interface IBookingService
    {

        public List<Booking> GetAllBookings();
        public Task<Booking> GetBookingById(int id);
        public List<Booking> GetBookingsByUserId(int userId);
        public Task DeleteBooking(int id);
        public Task CreateBooking(Booking booking);
        public Task UpdateBooking(Booking booking);

    }
}
