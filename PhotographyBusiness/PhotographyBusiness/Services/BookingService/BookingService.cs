using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.EFDbContext;
using PhotographyBusiness.MockData;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PhotographerTest")]

namespace PhotographyBusiness.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private IUserService _userService;
        private GenericDbService<Booking> _genericDbService;


        private List<Booking> _bookings;

        public int number { get; set; } // Arun: til sort, så den både kan sortere asc og desc med samme klik. 

        public BookingService(GenericDbService<Booking> genericDbService, IUserService userService)
        {
            _genericDbService = genericDbService;
            _userService = userService;
            _bookings = GetAllBookingsAsync().Result;
            //_bookings = MockBookings.GetAllMockBookings();
           //_genericDbService.SaveObjects(_bookings);

        }
        /// <summary>
        /// konsturtøren bliver brugt til unit test.
        /// MockData bliver brug ift. CRUD unittest. 
        /// </summary>
        public BookingService()
        {
            _bookings = MockBookings.GetAllMockBookings();
        }
        internal async Task<List<Booking>> GetAllBookingsAsync()
        {
            using (var context = new ObjectDbContext()) // Silas: vi skal også have useren med, når vi kalder på bookingen
            {
                return await context.Bookings.Include(b => b.User).AsNoTracking().ToListAsync();
            }
            
        }

        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

        public Booking GetBookingById(int id)
        {
            foreach (Booking booking in _bookings)
            {
                if (id == booking.BookingId)
                {
                    return booking;
                }
            }
            return null;
        }
        public Booking GetBookingById_User(int id)
        {
            foreach (Booking booking in _bookings)
            {
                if (id == booking.User.UserId)
                {
                    return booking;
                }
            }
            return null;
        }
        public List<Booking> GetBookingById_User_(int id)
        {
            var tempbookings = new List<Booking>();
            foreach(var user in _userService.GetAllUsers())
            {
                if (user.UserId == id)
                {
                    foreach (Booking booking in _bookings)
                    {
                        if (user.UserId == booking.UserId)
                        {
                            tempbookings.Add(booking);

                        }
                    }

                }
            
            }
          
            return _bookings = tempbookings;
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            IEnumerable<Booking> bookings = from booking in _bookings
                                            where booking.User.UserId == userId
                                            select booking;

            return bookings.ToList();
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            //Jeg har kommmentere den fordi pga. unittest
            await _genericDbService.AddObjectAsync(booking);
            booking.User = _userService.GetUserByIdAsync(Convert.ToInt32(booking.UserId)).Result; // Manually add the User object (Identity_Insert is set to off in the DB)
            if (booking != null)
            {
                this._bookings.Add(booking);

            }         
        }

        public async Task DeleteBooking(int id)
        {
            _bookings.Remove(GetBookingById(id));
            //await _genericDbService.DeleteObjectAsync(_genericDbService.GetObjectByIdAsync(id).Result);
            
        }

        public Task UpdateBooking(Booking booking)
        {
            foreach (Booking b in this._bookings)
            {
                if (b.BookingId == booking.BookingId)
                {
                    b.AdminNote = booking.AdminNote;
                    b.Category = booking.Category;
                    b.DateTimeOfEvent = booking.DateTimeOfEvent;
                    b.Address = booking.Address;
                    b.Price = booking.Price;
                    break;
                }
            }
            _genericDbService.UpdateObjectAsync(booking);

            return null;
        }

        public List<Booking> GetAllBookingsThisMonth()
        {
            return GetAllBookings().Where(b => b.DateTimeOfEvent >= DateTime.Now.AddDays(-30) && b.IsAccepted == true).ToList();
        }

        public List<Booking> GetUpcomingBookings()
        {
            return GetAllBookings().Where(b => b.IsAccepted == true && b.DateTimeOfEvent > DateTime.Now).ToList();
        }

        public List<Booking> GetMostRecentRequests()
        {
            return GetAllBookingsRequests().OrderByDescending(b => b.DateCreated).Take(5).ToList();
        }

        public List<Booking> GetAllBookingsRequests()
        {
            return GetAllBookings().Where(b => b.IsAccepted == false).ToList();
        }

        public async Task<List<Booking>> FilterBookingsByDate(DateTime startdate, DateTime endDate)
        {
            IEnumerable<Booking> filteredBookings = from booking in _bookings
                                                    where booking.DateTimeOfEvent >= startdate && booking.DateTimeOfEvent <= endDate
                                                    && booking.IsAccepted is true
                                                    select booking;



            //                                    from booking in Bookings
            //                                    where booking.Date >= startdate && booking.Date <= endDate
            //                                    select booking;



            return filteredBookings.ToList();
        }
        public async Task<List<Booking>> FilterBookingsByNameOrEmail(string nameinput)
        {
            if (string.IsNullOrEmpty(nameinput)) return GetAllBookings().Where(b => b.IsAccepted == true).ToList();

            IEnumerable<Booking> filteredBookings = from booking in _bookings
                                                    where booking.User.Name.Contains(nameinput) && booking.IsAccepted is true
                                                    || booking.User.Email.Contains(nameinput) && booking.IsAccepted is true
                                                    select booking;
            return filteredBookings.ToList();
        }

        // REDUNDANT EMAIL SEARCHING IMPLEMENTED IN NAME SEARCH

        //public async Task<List<Booking>> FilterBookingsByEmail(string Email)         
        //{
        //    IEnumerable<Booking> filteredBookings = from booking in Bookings
        //                                            where booking.User.Email.ToLower().Contains((Email))
        //                                            select booking;
        //    return filteredBookings.ToList();
        //}

        public async Task<List<Booking>> FilterBookingsByCategory(string Category)
        {
            IEnumerable<Booking> filteredBookings = from booking in _bookings
                                                    where booking.Category.Contains((Category))
                                                    && booking.IsAccepted is true
                                                    select booking;
            return filteredBookings.ToList();
        }


        public async Task<List<Booking>> SortBookingByCategory()
        {
            if (number == 1)
            {
                number--;
                IEnumerable<Booking> filteredBookingsdesc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.Category descending
                        select booking;

                return filteredBookingsdesc.ToList();
            }

            number++;
            IEnumerable<Booking> filteredBookingsasc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.Category ascending
                        select booking;

            return filteredBookingsasc.ToList();



        }

        public async Task<List<Booking>> SortBookingByDate()
        {
            if (number == 1)
            {
                number--;
                IEnumerable<Booking> filteredBookingsdesc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.DateTimeOfEvent descending
                        select booking;

                return filteredBookingsdesc.ToList();
            }

            number++;
            IEnumerable<Booking> filteredBookingsasc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.DateTimeOfEvent ascending
                        select booking;

            return filteredBookingsasc.ToList();



        }

        public async Task<List<Booking>> SortBookingByName()
        {
            if (number == 1)
            {
                number--;
                IEnumerable<Booking> filteredBookingsdesc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.User.Name descending
                        select booking;

                return filteredBookingsdesc.ToList();
            }

            number++;
            IEnumerable<Booking> filteredBookingsasc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.User.Name ascending
                        select booking;

            return filteredBookingsasc.ToList();



        }

        public async Task<List<Booking>> SortBookingByEmail()
        {
            if (number == 1)
            {
                number--;
                IEnumerable<Booking> filteredBookingsdesc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.User.Email descending
                        select booking;

                return filteredBookingsdesc.ToList();
            }

            number++;
            IEnumerable<Booking> filteredBookingsasc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.User.Email ascending
                        select booking;

            return filteredBookingsasc.ToList();



        }

    }
}
