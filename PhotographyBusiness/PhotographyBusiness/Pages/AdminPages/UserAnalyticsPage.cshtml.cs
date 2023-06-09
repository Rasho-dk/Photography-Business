using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services;
using System.Data;

namespace PhotographyBusiness.Pages.AdminPages
{
    [Authorize(Roles = "admin")]
    public class UserAnalyticsPageModel : PageModel
    {
        private GenericDbService<User> _genericDbService; // Using DB Service to have data be up to date

        // Numbered statistics
        public int TotalUsers { get; set; }
        public int UsersThisMonth { get; set; }

        // Charts
        public string ChartTotalUsers { get; internal set; }
        public string ChartsMonthlyUsers { get; internal set; }
        public string ChartUsersLastYear { get; internal set; }

        // Lists and misc.
        public IEnumerable<User> Users { get; set; }

        private int ChartsHeight = 22;

        public UserAnalyticsPageModel(GenericDbService<User> genericDbService)
        {
            _genericDbService = genericDbService;
        }

        public IActionResult OnGet()
        {
            Users = _genericDbService.GetObjectsAsync().Result;
            
            // Numbered statistics
            TotalUsers = Users.Count();
            UsersThisMonth = Users.Where(u => u.DateCreated > DateTime.Now.AddDays(-30)).Count();;

            // Total users chart
            DataTable totalUsersData = new DataTable();
            totalUsersData.Columns.Add("Type", typeof(System.String));
            totalUsersData.Columns.Add("Amount", typeof(System.Int32));

            totalUsersData.Rows.Add("Users", TotalUsers);

            StaticSource totalUsersSource = new StaticSource(totalUsersData);
            DataModel totalUsersModel = new DataModel();
            totalUsersModel.DataSources.Add(totalUsersSource);
            Charts.ColumnChart totalUsersChart = new Charts.ColumnChart("totalUsersChart");

            totalUsersChart.Width.Em(ChartsHeight); totalUsersChart.Height.Em(ChartsHeight);
            totalUsersChart.Data.Source = totalUsersModel;
            totalUsersChart.Caption.Text = "Total users";
            totalUsersChart.Legend.Show = false;
            totalUsersChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartTotalUsers = totalUsersChart.Render();

            // Users this month
            DataTable monthlyUsersData = new DataTable();
            monthlyUsersData.Columns.Add("Type", typeof(System.String));
            monthlyUsersData.Columns.Add("Amount", typeof(System.Int32));

            monthlyUsersData.Rows.Add("Users", UsersThisMonth);

            StaticSource monthlyUsersSource = new StaticSource(monthlyUsersData);
            DataModel monthlyUsersModel = new DataModel();
            monthlyUsersModel.DataSources.Add(monthlyUsersSource);
            Charts.ColumnChart monthlyUsersChart = new Charts.ColumnChart("monthlyUsersChart");

            monthlyUsersChart.Width.Em(ChartsHeight); monthlyUsersChart.Height.Em(ChartsHeight);
            monthlyUsersChart.Data.Source = monthlyUsersModel;
            monthlyUsersChart.Caption.Text = "Users registered last month";
            monthlyUsersChart.SubCaption.Text = "Last 30 days";
            monthlyUsersChart.Legend.Show = false;
            monthlyUsersChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartsMonthlyUsers = monthlyUsersChart.Render();


            // Users last year
            DataTable usersLastYear = new DataTable();
            usersLastYear.Columns.Add("Month", typeof(System.String));
            usersLastYear.Columns.Add("Users", typeof(System.Double));

            // Put all users into an enumerable collection 

            // Display the month and year going 12 months back. Display the count of users who were created in the given timespan
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-11).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-11).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-11).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-10).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-10).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-10).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-9).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-9).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-9).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-8).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-8).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-8).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-7).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-7).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-7).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-6).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-6).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-6).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-5).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-5).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-5).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-4).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-4).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-4).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-3).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-3).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-3).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-2).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-2).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-2).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.AddMonths(-1).ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.AddMonths(-1).Month && x.DateCreated.Year == DateTime.Now.AddMonths(-1).Year).Count());
            usersLastYear.Rows.Add(DateTime.Now.ToString("MMMM yy"), Users.Where(x => x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).Count());

            StaticSource usersLastYearSource = new StaticSource(usersLastYear);
            DataModel usersLastYearModel = new DataModel();
            usersLastYearModel.DataSources.Add(usersLastYearSource);
            Charts.ColumnChart userLastYearChart = new Charts.ColumnChart("monthlyUsersChartLastYear");

            userLastYearChart.Width.Em(80);
            userLastYearChart.Height.Em(ChartsHeight);

            userLastYearChart.Data.Source = usersLastYearModel;
            userLastYearChart.Caption.Text = "Users registered by month";
            userLastYearChart.SubCaption.Text = "Last 12 months";
            userLastYearChart.Legend.Show = false;
            userLastYearChart.XAxis.Text = "Month";
            userLastYearChart.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ChartUsersLastYear = userLastYearChart.Render();

            return Page();
        }
    }
}
