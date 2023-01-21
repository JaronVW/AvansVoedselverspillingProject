using Core.DomainServices;
using Infrastructure;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using WebAPI.GraphQl;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(
    options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); }
);

builder.Services.AddScoped<IMealBoxRepository, MealBoxEFRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenEFRepository>();
builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
builder.Services.AddScoped<IStudentRepository, StudentEFRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeEFRepository>();

builder.Services.AddScoped<Query>();
builder.Services.AddGraphQLServer().AddQueryType<Query>();


builder.Services.AddAuthorization();
var app = builder.Build();
app.MapGraphQL();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();