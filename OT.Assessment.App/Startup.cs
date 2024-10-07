using Microsoft.EntityFrameworkCore;
using OT.Assessment.App.Data;
using OT.Assessment.App.Services;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration; // Assigning the passed configuration
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Register DbContext with connection string from appsettings.json
        services.AddDbContext<OTDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                   .EnableSensitiveDataLogging()); // Add this line for detailed logs

        // Register RabbitMQ service
        services.AddSingleton<IRabbitMQService, RabbitMQService>(); // Register RabbitMQService

        // Add logging service
        services.AddLogging(); // Register logging service

        // Other services...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Middleware setup...
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}