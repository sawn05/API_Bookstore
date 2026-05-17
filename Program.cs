using API_Bookstore.Data;
using API_Bookstore.Repositories;
using API_Bookstore.Repositories.Interfaces;
using API_Bookstore.Services;
using API_Bookstore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with SQL Server connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

// Register repositories for dependency injection
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


// Register services for dependency injection
builder.Services.AddScoped<ICategoryService, CategoryService>();



var app = builder.Build();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Enable authentication and authorization middleware
// app.UseAuthorization();

app.MapControllers();


app.Run();
