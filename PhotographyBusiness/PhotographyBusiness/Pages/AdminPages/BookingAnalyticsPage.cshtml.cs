using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using System.Data;

namespace PhotographyBusiness.Pages.AdminPages
{
    [Authorize(Roles = "admin")]
    public class BookingAnalyticsPageModel : PageModel
    {
        private IBookingService _bookingService;
        
        // Charts
        public string ChartBookingCategory { get; internal set; }
        public string PieChartBookingAcceptedCompare { get; internal set; }
        public string ChartBookingAcceptedCompare { get; internal set; }
        public string PieChartBookingCategory { get; internal set; }
        public string ChartMonthlyBookingCategory { get; internal set; }
        public string PieChartMonthlyBookingCategory { get; internal set; }
        public string ChartMonthlyBookings { get; internal set; }

        // Numbered statistics
        public int TotalBookings { get; set; }
        public int BookingsThisMonth { get; set; }
        public int CompletedBookingsThisMonth { get; set; }
        public int PendingRequests { get; set; }

        // Lists and misc.
        public IEnumerable<Booking> Bookings { get; set; }
        private int ChartsHeight = 22;

        public BookingAnalyticsPageModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void OnGet()
        {
            Bookings = _bookingService.GetAllBookingsAsync().Result;

            #region Numbered statistics

            TotalBookings = Bookings.Where(b => b.IsAccepted == true).ToList().Count(); // Total bookings
            BookingsThisMonth = _bookingService.GetAllBookingsThisMonth().Count(); // Bookings last 30 days
            PendingRequests = _bookingService.GetAllBookingsRequests().Count();
            CompletedBookingsThisMonth = Bookings.Where(b => b.Date < DateTime.Now && b.IsAccepted == true).Count();

            #endregion

            #region Booking category charts
            // create data table to store data
            DataTable categoryData = new DataTable();
            // Add columns to data table
            categoryData.Columns.Add("Categories", typeof(System.String));
            categoryData.Columns.Add("Bookings", typeof(System.Int32));
            // Add rows to data table

            categoryData.Rows.Add("Weddings", Bookings.Where(b => b.Category == "Wedding").Count());
            categoryData.Rows.Add("Parties", Bookings.Where(b => b.Category == "Party").Count());
            categoryData.Rows.Add("Portraits", Bookings.Where(b => b.Category == "Portrait").Count());
            categoryData.Rows.Add("Fashion", Bookings.Where(b => b.Category == "Fashion").Count());
            categoryData.Rows.Add("Food", Bookings.Where(b => b.Category == "Food").Count());
            categoryData.Rows.Add("Events", Bookings.Where(b => b.Category == "Event").Count());

            // Create static categorySource with this data table
            StaticSource categorySource = new StaticSource(categoryData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(categorySource);
            // Instantiate Column Chart
            Charts.ColumnChart categoryChart = new Charts.ColumnChart("categoryChart");
            // Set Chart's width and height
            categoryChart.Width.Em(30);
            categoryChart.Height.Em(ChartsHeight);
            // Set DataModel instance as the data categorySource of the chart
            categoryChart.Data.Source = model;
            // Set Chart Title
            categoryChart.Caption.Text = "Most popular booking categories";
            // Set chart sub title
            categoryChart.SubCaption.Text = "2023-Now";
            // hide chart Legend
            categoryChart.Legend.Show = false;
            // set XAxis Text
            categoryChart.XAxis.Text = "Category";
            // Set YAxis title
            categoryChart.YAxis.Text = "Amount";
            // set chart theme
            categoryChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            // set chart rendering json
            ChartBookingCategory = categoryChart.Render();

            // Intantiate pie chart

            Charts.PieChart categoryPieChart = new Charts.PieChart("categoryPieChart");
            categoryPieChart.Width.Em(ChartsHeight); categoryPieChart.Height.Em(ChartsHeight);
            categoryPieChart.Data.Source = model;
            categoryPieChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            categoryPieChart.Caption.Text = "Distribution of booking category";
            categoryPieChart.SubCaption.Text = "2023-Now";

            PieChartBookingCategory = categoryPieChart.Render();

            #endregion

            #region Accepted/Not-accepted bookings charts
            // Accepted vs non-accepted bookings

            // Column chart

            DataTable acceptedData = new DataTable();

            acceptedData.Columns.Add("State", typeof(System.String));
            acceptedData.Columns.Add("Amount", typeof(System.Int32));

            acceptedData.Rows.Add("Accepted", Bookings.Where(b => b.IsAccepted == true).Count());
            acceptedData.Rows.Add("Not-accepted", Bookings.Where(b => b.IsAccepted == false).Count());
            acceptedData.Rows.Add("Total", Bookings.Count());

            StaticSource acceptedSource = new StaticSource(acceptedData);
            DataModel acceptedModel = new DataModel();

            acceptedModel.DataSources.Add(acceptedSource);
            Charts.ColumnChart acceptedChart = new Charts.ColumnChart("acceptedChart");

            acceptedChart.Width.Em(ChartsHeight); acceptedChart.Height.Em(ChartsHeight);
            acceptedChart.Data.Source = acceptedModel;
            acceptedChart.Caption.Text = "Accepted vs non-accepted bookings";
            acceptedChart.SubCaption.Text = "2023-Now";
            acceptedChart.Legend.Show = false;
            acceptedChart.YAxis.Text = "Amount";
            acceptedChart.XAxis.Text = "State";
            acceptedChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartBookingAcceptedCompare = acceptedChart.Render();

            // Pie chart

            DataTable acceptedPieChartData = new DataTable();

            acceptedPieChartData.Columns.Add("State", typeof(System.String));
            acceptedPieChartData.Columns.Add("Count", typeof(System.Int32));

            acceptedPieChartData.Rows.Add("Accepted", Bookings.Where(b => b.IsAccepted == true).Count());
            acceptedPieChartData.Rows.Add("Not-accepted", Bookings.Where(b => b.IsAccepted == false).Count());

            StaticSource acceptedSourcePieChart = new StaticSource(acceptedPieChartData);
            DataModel acceptedPieModel = new DataModel();

            acceptedPieModel.DataSources.Add(acceptedSourcePieChart);

            Charts.PieChart acceptedPieChart = new Charts.PieChart("acceptedPieChart");

            acceptedPieChart.Width.Em(ChartsHeight); acceptedPieChart.Height.Em(ChartsHeight);
            acceptedPieChart.Data.Source = acceptedPieModel;
            acceptedPieChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            acceptedPieChart.Caption.Text = "Distribution of accepted-state";
            acceptedPieChart.SubCaption.Text = "2023-Now";

            PieChartBookingAcceptedCompare = acceptedPieChart.Render();

            #endregion

            #region Monthly charts

            DataTable monthlyBookingsCategoryData = new DataTable();
            monthlyBookingsCategoryData.Columns.Add("Categories", typeof(System.String));
            monthlyBookingsCategoryData.Columns.Add("Bookings", typeof(System.Int32));

            monthlyBookingsCategoryData.Rows.Add("Weddings", Bookings.Where(b => b.Category == "Wedding" && b.DateCreated > DateTime.Now.AddDays(-30)).Count());
            monthlyBookingsCategoryData.Rows.Add("Parties", Bookings.Where(b => b.Category == "Party" && b.DateCreated > DateTime.Now.AddDays(-30)).Count());
            monthlyBookingsCategoryData.Rows.Add("Portraits", Bookings.Where(b => b.Category == "Portrait" && b.DateCreated > DateTime.Now.AddDays(-30)).Count());
            monthlyBookingsCategoryData.Rows.Add("Fashion", Bookings.Where(b => b.Category == "Fashion" && b.DateCreated > DateTime.Now.AddDays(-30)).Count());
            monthlyBookingsCategoryData.Rows.Add("Food", Bookings.Where(b => b.Category == "Food" && b.DateCreated > DateTime.Now.AddDays(-30)).Count());
            monthlyBookingsCategoryData.Rows.Add("Events", Bookings.Where(b => b.Category == "Event" && b.DateCreated > DateTime.Now.AddDays(-30)).Count());

            StaticSource monthlyBookingCategorySource = new StaticSource(monthlyBookingsCategoryData);
            DataModel monthlyBookingCategoryModel = new DataModel();
            monthlyBookingCategoryModel.DataSources.Add(monthlyBookingCategorySource);

            Charts.ColumnChart monthlyBookingCategoryChart = new Charts.ColumnChart("monthlyBookingCategoryChart");

            monthlyBookingCategoryChart.Width.Em(30);
            monthlyBookingCategoryChart.Height.Em(ChartsHeight);
            monthlyBookingCategoryChart.Data.Source = monthlyBookingCategoryModel;
            monthlyBookingCategoryChart.Caption.Text = "Most popular booking categories this month";
            monthlyBookingCategoryChart.SubCaption.Text = "Last 30 days";
            monthlyBookingCategoryChart.Legend.Show = false;
            monthlyBookingCategoryChart.YAxis.Text = "Count";
            monthlyBookingCategoryChart.XAxis.Text = "Category";
            monthlyBookingCategoryChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartMonthlyBookingCategory = monthlyBookingCategoryChart.Render();

            Charts.PieChart monthlyCategoryPieChart = new Charts.PieChart("monthlyCategoryPieChart");
            monthlyCategoryPieChart.Width.Em(ChartsHeight); monthlyCategoryPieChart.Height.Em(ChartsHeight);
            monthlyCategoryPieChart.Data.Source = monthlyBookingCategoryModel;
            monthlyCategoryPieChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            monthlyCategoryPieChart.Caption.Text = "Distribution of booking category this month";
            monthlyCategoryPieChart.SubCaption.Text = "Last 30 days";

            PieChartMonthlyBookingCategory = monthlyCategoryPieChart.Render();

            // Booking count by month last year

            DataTable bookingsLastYear = new DataTable();
            bookingsLastYear.Columns.Add("Month", typeof(System.String));
            bookingsLastYear.Columns.Add("Bookings", typeof(System.Double));

            // Display the month and year for the last 12 months. Display the count of bookings in that timespan.
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-11).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-11).Month && x.Date.Year == DateTime.Now.AddMonths(-11).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-10).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-10).Month && x.Date.Year == DateTime.Now.AddMonths(-10).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-9).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-9).Month && x.Date.Year == DateTime.Now.AddMonths(-9).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-8).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-8).Month && x.Date.Year == DateTime.Now.AddMonths(-8).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-7).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-7).Month && x.Date.Year == DateTime.Now.AddMonths(-7).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-6).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-6).Month && x.Date.Year == DateTime.Now.AddMonths(-6).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-5).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-5).Month && x.Date.Year == DateTime.Now.AddMonths(-5).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-4).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-4).Month && x.Date.Year == DateTime.Now.AddMonths(-4).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-3).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-3).Month && x.Date.Year == DateTime.Now.AddMonths(-3).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-2).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-2).Month && x.Date.Year == DateTime.Now.AddMonths(-2).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.AddMonths(-1).ToString("MMMM yy"), Bookings.Where(x => x.Date.Month == DateTime.Now.AddMonths(-1).Month && x.Date.Year == DateTime.Now.AddMonths(-1).Year).Count());
            bookingsLastYear.Rows.Add(DateTime.Now.ToString("MMMM yy"), Bookings.Where(x => x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).Count());

            StaticSource bookingsLastYearSource = new StaticSource(bookingsLastYear);
            DataModel bookingsLastYearModel = new DataModel();
            bookingsLastYearModel.DataSources.Add(bookingsLastYearSource);
            Charts.ColumnChart bookingsLastYearChart = new Charts.ColumnChart("monthlyBookingsChartLastYear");

            bookingsLastYearChart.Width.Percentage(80);
            bookingsLastYearChart.Height.Em(ChartsHeight);

            bookingsLastYearChart.Data.Source = bookingsLastYearModel;
            bookingsLastYearChart.Caption.Text = "Booking count by month";
            bookingsLastYearChart.SubCaption.Text = "Last 12 months";
            bookingsLastYearChart.Legend.Show = false;
            bookingsLastYearChart.XAxis.Text = "Month";
            bookingsLastYearChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartMonthlyBookings = bookingsLastYearChart.Render();

            #endregion
        }
    }
}
