using HotelAndRestaurant.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddSwaggerGen();


// MongoDB connection string
var mongoConnectionString = "mongodb://localhost:27017";
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));

//ConnectionString
builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDBConnection"));
    }
    );


var app = builder.Build();


app.UseCors(policy => policy.AllowAnyHeader()
                               .AllowAnyMethod()
                               .SetIsOriginAllowed(origin => true)
                               .AllowCredentials()

                               );


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