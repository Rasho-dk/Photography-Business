﻿using Microsoft.EntityFrameworkCore;
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
        }

        /// <summary>
        /// konsturtøren bliver brugt til unit test.
        /// MockData bliver brug ift. CRUD unittest. 
        /// </summary>
        public BookingService()
        {
            _bookings = MockBookings.GetAllMockBookings();
        }

        /// <summary>
        /// Internal method for returning the user object inside the booking
        /// </summary>
        /// <returns></returns>
        internal async Task<List<Booking>> GetAllBookingsAsync()
        {
            using (var context = new ObjectDbContext())
            {
                return await context.Bookings.Include(b => b.User).AsNoTracking().ToListAsync();
            }
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            if (booking != null)
            {
               // await _genericDbService.AddObjectAsync(booking);
                // To accomodate Identity_Insert = OFF, we need to manually instantiate the user by the id that was passed in the request page
                // Otherwise we will get an SQLException
                //Sbooking.User = _userService.GetUserByIdAsync(booking.UserId).Result;
                this._bookings.Add(booking);
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

        //Shero
        public Booking GetBookingById_User(int id,int bId)
        {
            foreach (Booking booking in _bookings)
            {
                if (id == booking.User.UserId && bId == booking.BookingId)
                {
                    return booking;
                }
            }
            return null;
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            IEnumerable<Booking> bookings = from booking in _bookings
                                            where booking.User.UserId == userId
                                            select booking;

            return bookings.ToList();
        }

        public async Task DeleteBooking(int id)
        {
            _bookings.Remove(GetBookingById(id));
            await _genericDbService.DeleteObjectAsync(_genericDbService.GetObjectByIdAsync(id).Result);
        }

        public async Task UpdateBooking(Booking booking)
        {
            
            foreach (Booking b in this._bookings)
            {
                if (b.BookingId == booking.BookingId)
                {
                    b.AdminNote = booking.AdminNote;
                    b.Category = booking.Category;
                    b.Date = booking.Date;
                    b.Address = booking.Address;
                    b.Price = booking.Price;
                    b.IsAccepted = booking.IsAccepted;
                }
            }

            await _genericDbService.UpdateObjectAsync(booking);
        }

        public List<Booking> GetAllBookingsThisMonth()
        {
            return GetAllBookings().Where(b => b.Date >= DateTime.Now.AddDays(-30) && b.IsAccepted == true).ToList();
        }

        public List<Booking> GetUpcomingBookings()
        {
            return GetAllBookings().Where(b => b.IsAccepted == true && b.Date > DateTime.Now).ToList();
        }

        public List<Booking> GetMostRecentRequests()
        {
            return GetAllBookingsRequests().OrderBy(b => b.DateCreated).Take(5).ToList();
        }

        public List<Booking> GetAllBookingsRequests()
        {
            return GetAllBookings().Where(b => b.IsAccepted == false).ToList();
        }

        public async Task<List<Booking>> FilterBookingsByDate(DateTime startdate, DateTime endDate)
        {
            IEnumerable<Booking> filteredBookings = from booking in _bookings
                                                    where booking.Date >= startdate && booking.Date <= endDate
                                                    && booking.IsAccepted is true
                                                    select booking;

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
                        orderby booking.Date descending
                        select booking;

                return filteredBookingsdesc.ToList();
            }

            number++;
            IEnumerable<Booking> filteredBookingsasc =

                        from booking in _bookings
                        where booking.IsAccepted is true
                        orderby booking.Date ascending
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
