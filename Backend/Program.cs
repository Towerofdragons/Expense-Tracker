using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TrackerDBContext>(
    options => options.UseInMemoryDatabase("TrackerDB")
);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<TrackerDBContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Ensure the database is created and seed data is applied
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TrackerDBContext>();
    context.Database.EnsureCreated();  // Forces database creation & applies seed data
}


// Configure the HTTP request pipeline.
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tracker}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
