using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System.Data;

namespace PhotographyBusiness.Pages.AdminPages
{
    [Authorize(Roles = "admin")]
    public class AnalyticsPageModel : PageModel
    {
        private IBookingService _bookingService;
        private IUserService _userService;
        private GenericDbService<User> _genericDbService;

        // Charts
        public string ChartBookingCategory { get; internal set; }
        public string PieChartBookingAcceptedCompare { get; internal set; }
        public string ChartBookingAcceptedCompare { get; internal set; }
        public string PieChartBookingCategory { get; internal set; }
        public string ChartMonthlyBookingCategory { get; internal set; }
        public string PieChartMonthlyBookingCategory { get; internal set; }
        public string ChartMonthlyRevenue { get; internal set; }

        // Numbered statistics
        public int TotalUsers { get; set; }
        public int TotalBookings { get; set; }
        public int BookingsThisMonth { get; set; }
        public int PendingRequests { get; set; }

        public double TotalRevenue { get; set; }
        public double MonthlyRevenue { get; set; }

        // Lists and misc.
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<User> Users { get; set; }

        public int ChartsHeight = 22;

        public AnalyticsPageModel(IBookingService bookingService, IUserService userService, GenericDbService<User> genericDbService)
        {
            _bookingService = bookingService;
            _userService = userService;
            _genericDbService = genericDbService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Bookings = _bookingService.GetAllBookingsAsync().Result;
            Users = _genericDbService.GetObjectsAsync().Result;
            
            #region Numbered statistics

            TotalUsers = Users.Count();
            TotalBookings = Bookings.Where(b => b.IsAccepted == true).ToList().Count(); // Total bookings
            BookingsThisMonth = _bookingService.GetAllBookingsThisMonth().Count(); // Bookings last 30 days
            PendingRequests = _bookingService.GetAllBookingsRequests().Count();

            IEnumerable<Booking> bookingsList = new List<Booking>();
            bookingsList = _bookingService.GetAllBookingsThisMonth().Where(x => x.Date <= DateTime.Now); // Get all bookings for this month that have happened before today. Meaning the event has taken place.

            foreach (Booking booking in bookingsList)
            {
                if (booking.Price != null && booking.IsAccepted == true)
                {
                    MonthlyRevenue += Convert.ToDouble(booking.Price);
                }
            }

            // Get all bookings with a date before today. Meaning the event has taken place.
            bookingsList = Bookings.Where(x => x.Date <= DateTime.Now); 

            foreach (Booking booking in bookingsList)
            {
                if (booking.Price != null && booking.IsAccepted == true)
                {
                    TotalRevenue += Convert.ToDouble(booking.Price);
                }
            }

            #endregion

            #region Revenue charts

            DataTable revenueMonthlyData = new DataTable();
            revenueMonthlyData.Columns.Add("Month", typeof(System.String));
            revenueMonthlyData.Columns.Add("Revenue", typeof(System.Double));

            // Get an enumerable collection of all the prices for each booking for the specific month and year.
            IEnumerable<double> revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-1).Month && DateTime.Now.AddMonths(-11).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            // Display the month and year going back 12 months. Add up revenue using .Sum()
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-11).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-10).Month && DateTime.Now.AddMonths(-10).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-10).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-9).Month && DateTime.Now.AddMonths(-9).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-9).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-8).Month && DateTime.Now.AddMonths(-8).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-8).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-7).Month && DateTime.Now.AddMonths(-7).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-7).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-6).Month && DateTime.Now.AddMonths(-6).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-6).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-5).Month && DateTime.Now.AddMonths(-5).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-5).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-4).Month && DateTime.Now.AddMonths(-4).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-4).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-3).Month && DateTime.Now.AddMonths(-3).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-3).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-2).Month && DateTime.Now.AddMonths(-2).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-2).ToString("MMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.AddMonths(-1).Month && DateTime.Now.AddMonths(-1).Year == x.Date.Year && x.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.AddMonths(-1).ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(x => x.Date.Month == DateTime.Now.Month && DateTime.Now.Year == x.Date.Year && x.IsAccepted).Select(x => Convert.ToDouble(x.Price));
            revenueMonthlyData.Rows.Add(DateTime.Now.ToString("MMMM yy"), revenueList.Sum(x => Convert.ToDouble(x)));
            
            StaticSource monthlyRevenueSource = new StaticSource(revenueMonthlyData);
            DataModel monthlyRevenueModel = new DataModel();
            monthlyRevenueModel.DataSources.Add(monthlyRevenueSource);
            Charts.ColumnChart monthlyRevenueChart = new Charts.ColumnChart("monthlyRevenueChart");

            monthlyRevenueChart.Width.Percentage(80);
            monthlyRevenueChart.Height.Em(ChartsHeight);

            monthlyRevenueChart.Data.Source = monthlyRevenueModel;
            monthlyRevenueChart.Caption.Text = "Revenue by month";
            monthlyRevenueChart.SubCaption.Text = "Last 12 months";
            monthlyRevenueChart.Legend.Show = false;
            monthlyRevenueChart.XAxis.Text = "Month";
            monthlyRevenueChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartMonthlyRevenue = monthlyRevenueChart.Render();

            #endregion

            return Page();
        }
    }
}
