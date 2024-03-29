using ApplicationServices;
using Core.DomainServices;
using Infrastructure;
using Infrastructure.ContextClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add ServicesLayer to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(
    options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); }
);


builder.Services.AddDbContext<AppIdentityDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDB")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppIdentityDBContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMealBoxRepository, MealBoxEFRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenEFRepository>();
builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
builder.Services.AddScoped<IStudentRepository, StudentEFRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeEFRepository>();
builder.Services.AddScoped<IMealBoxService, MealBoxService>();



var app = builder.Build();

// Configure the HTTP request pipeline.


// app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();