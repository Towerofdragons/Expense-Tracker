using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Step 1: Configure SQL Server connection
builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Step 2: Add services to the container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Step 3: Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Step 4: Configure Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tracker}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
