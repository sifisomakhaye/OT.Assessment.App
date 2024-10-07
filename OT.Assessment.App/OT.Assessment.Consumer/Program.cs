using Microsoft.EntityFrameworkCore;
using OT.Assessment.App.Data;  
using OT.Assessment.App.Services;

var builder = WebApplication.CreateBuilder(args);

// Services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add DbContext to DI container with the connection string from appsettings.json
builder.Services.AddDbContext<OTDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();
builder.Services.AddTransient<IDatabaseService, DatabaseService>(); 

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Configure error handling
    app.UseHsts(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Maping controllers

app.Run();