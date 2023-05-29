using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PhotographyBusiness.EFDbContext;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services;
using PhotographyBusiness.Services.AlbumService;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.MailService;
using PhotographyBusiness.Services.PhotoService;
using PhotographyBusiness.Services.UserService;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<GenericDbService<User>>();
//builder.Services.AddSingleton<GenericDbService<Booking>>();
builder.Services.AddTransient<GenericDbService<Booking>>();

builder.Services.AddSingleton<IBookingService, BookingService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IMailService, MailService>();

builder.Services.AddTransient<IAlbumService,AlbumService>();
builder.Services.AddTransient<IPhotoService, PhotoService>();


builder.Services.AddDbContext<ObjectDbContext>();


builder.Services.Configure<CookiePolicyOptions>(options =>
{

    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;

}); builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions =>
{
    cookieOptions.LoginPath = "/AccountPages/LogInPage";
    cookieOptions.AccessDeniedPath = "/AccountPages/AccessDeniedPage";   
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy =>
           policy.RequireClaim(ClaimTypes.Role, "admin"));
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/BookingPages");
    options.Conventions.AuthorizeFolder("/...");
    options.Conventions.AllowAnonymousToPage("/BookingPages/InvoicePage");
});
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
//app.UseAuthentication();

app.MapRazorPages();

app.Run();
