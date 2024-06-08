using Microsoft.AspNetCore.Identity;
using WebsiteTinhThanFoundation.Data;
using WebsiteTinhThanFoundation.Helpers;
using WebsiteTinhThanFoundation.ServiceExtension;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
}
// Add services to the container.
builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddControllersWithViews();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.ExpireTimeSpan = TimeSpan.FromDays(654);
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/admin/account/login";
    options.AccessDeniedPath = "/Home/Error";
    options.SlidingExpiration = true;
});

AddAuthorizationPolicies();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapAreaControllerRoute(
    name: "admin_default",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Admin));
        options.AddPolicy(Constants.Policies.RequireStaff, policy => policy.RequireRole(Constants.Roles.Admin, Constants.Roles.Staff));
        options.AddPolicy(Constants.Policies.RequireVolunteer, policy => policy.RequireRole(Constants.Roles.Admin, Constants.Roles.Staff, Constants.Roles.Volunteer));
    });
}