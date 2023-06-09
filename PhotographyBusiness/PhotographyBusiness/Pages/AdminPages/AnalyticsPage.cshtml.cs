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
        public string ChartCategoryRevenue { get; internal set; }
        public string ChartCategoryRevenueMonthly { get; internal set; }

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

            monthlyRevenueChart.Width.Em(80);
            monthlyRevenueChart.Height.Em(ChartsHeight);

            monthlyRevenueChart.Data.Source = monthlyRevenueModel;
            monthlyRevenueChart.Caption.Text = "Revenue by month";
            monthlyRevenueChart.SubCaption.Text = "Last 12 months";
            monthlyRevenueChart.Legend.Show = false;
            monthlyRevenueChart.XAxis.Text = "Month";
            monthlyRevenueChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartMonthlyRevenue = monthlyRevenueChart.Render();

            // Most revenue per category chart

            DataTable categoryRevenueData = new DataTable();
            categoryRevenueData.Columns.Add("Category", typeof(System.String));
            categoryRevenueData.Columns.Add("Revenue", typeof(System.Double));

            revenueList = bookingsList.Where(b => b.Category == "Wedding" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueData.Rows.Add("Weddings", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Party" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueData.Rows.Add("Parties", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Portrait" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueData.Rows.Add("Portraits", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Fashion" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueData.Rows.Add("Fashion", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Food" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueData.Rows.Add("Food", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Event" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueData.Rows.Add("Events", revenueList.Sum(x => Convert.ToDouble(x)));

            StaticSource categoryRevenueSource = new StaticSource(categoryRevenueData);
            DataModel categoryRevenueModel = new DataModel();
            categoryRevenueModel.DataSources.Add(categoryRevenueSource);
            Charts.ColumnChart categoryRevenueChart = new Charts.ColumnChart("categoryRevenueChart");

            categoryRevenueChart.Width.Em(40);
            categoryRevenueChart.Height.Em(ChartsHeight);

            categoryRevenueChart.Data.Source = categoryRevenueModel;
            categoryRevenueChart.Caption.Text = "Revenue by category";
            categoryRevenueChart.SubCaption.Text = "Lifetime";
            categoryRevenueChart.Legend.Show = false;
            categoryRevenueChart.XAxis.Text = "Category";
            categoryRevenueChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartCategoryRevenue = categoryRevenueChart.Render();

            // Revenue by category last month

            DataTable categoryRevenueMonthlyData = new DataTable();
            categoryRevenueMonthlyData.Columns.Add("Category", typeof(System.String));
            categoryRevenueMonthlyData.Columns.Add("Revenue", typeof(System.Double));

            revenueList = bookingsList.Where(b => b.Category == "Wedding" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueMonthlyData.Rows.Add("Weddings", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Party" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueMonthlyData.Rows.Add("Parties", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Portrait" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueMonthlyData.Rows.Add("Portraits", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Fashion" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueMonthlyData.Rows.Add("Fashion", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Food" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueMonthlyData.Rows.Add("Food", revenueList.Sum(x => Convert.ToDouble(x)));

            revenueList = bookingsList.Where(b => b.Category == "Event" && b.IsAccepted == true).Select(x => Convert.ToDouble(x.Price));
            categoryRevenueMonthlyData.Rows.Add("Events", revenueList.Sum(x => Convert.ToDouble(x)));

            StaticSource categoryRevenueMonthlySource = new StaticSource(categoryRevenueMonthlyData);
            DataModel categoryRevenueMonthlyModel = new DataModel();
            categoryRevenueMonthlyModel.DataSources.Add(categoryRevenueMonthlySource);
            Charts.ColumnChart categoryRevenueMonthlyChart = new Charts.ColumnChart("categoryRevenueChartMonthly");

            categoryRevenueMonthlyChart.Width.Em(40);
            categoryRevenueMonthlyChart.Height.Em(ChartsHeight);

            categoryRevenueMonthlyChart.Data.Source = categoryRevenueMonthlyModel;
            categoryRevenueMonthlyChart.Caption.Text = "Revenue by category";
            categoryRevenueMonthlyChart.SubCaption.Text = "Last 30 days";
            categoryRevenueMonthlyChart.Legend.Show = false;
            categoryRevenueMonthlyChart.XAxis.Text = "Category";
            categoryRevenueMonthlyChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartCategoryRevenueMonthly = categoryRevenueMonthlyChart.Render();

            #endregion

            return Page();
        }
    }
}
