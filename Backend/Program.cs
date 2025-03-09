using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Configure SQL Server connection
// builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
// );

// Configure In memory db
builder.Services.AddDbContext<TrackerDBContext>(
    options => options.UseInMemoryDatabase("TrackerDB")
);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<TrackerDBContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/LogIn";  
    options.AccessDeniedPath = "/Home/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

//Add services to the container
builder.Services.AddControllersWithViews();

//Authentication Middleware
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(options =>
//     {
//         options.LoginPath = "/Account/LogIn";  // Redirect to login if unauthorized
//         options.AccessDeniedPath = "/Home/AccessDenied";
//         options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//         options.SlidingExpiration = true;
//     });


var app = builder.Build();

// Ensure the database is created and seed data is applied
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TrackerDBContext>();
    context.Database.EnsureCreated();  // Forces database creation & applies seed data
}


// Configure the HTTP request pipeline.
// Step 3: Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Step 4: Configure Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=LogIn}")
    .WithStaticAssets();


app.Run();
