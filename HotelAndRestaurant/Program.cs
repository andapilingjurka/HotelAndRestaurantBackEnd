using HotelAndRestaurant;
using HotelAndRestaurant.Data;
using HotelAndRestaurant.Models.Hubs;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB connection string
var mongoConnectionString = "mongodb://localhost:27017";
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));

// ConnectionString
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDBConnection"));
    }
);

// Add SignalR
builder.Services.AddSignalR();

// Add Stripe configuration (Assuming you have an extension method for this)
builder.Services.AddStripeInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure CORS
app.UseCors(policy => policy.AllowAnyHeader()
                               .AllowAnyMethod()
                               .SetIsOriginAllowed(origin => true)
                               .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Make sure authentication is added before authorization
app.UseAuthorization();

app.MapControllers();

// Map SignalR hub
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
