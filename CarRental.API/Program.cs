using CarRental.Application.Interfaces;
using CarRental.Application.Models;
using CarRental.Application.Services;
using CarRental.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BookingPriceCalculationData>(
    builder.Configuration.GetSection("BookingPriceCalculationData"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<BookingPriceCalculationData>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddSingleton<BookingsDataStore>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
