using EasyTread.API.Hubs;
using EasyTread.Database;
using EasyTread.Services.Interface.mappers;
using EasyTread.Services.Interface.services;
using EasyTread.Services.Mappers;
using EasyTread.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddSwaggerDocument();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICrossingMapper, CrossingMapper>();
builder.Services.AddScoped<ICrossingService, CrossingService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IVehicleMapper, VehicleMapper>();
builder.Services.AddSignalR();

builder.Services.AddTransient<ErrorHandlingMiddleware>();
builder.Services.AddDbContext<EasyTreadContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseCors(policy => policy.AllowAnyHeader()
.AllowAnyMethod()
.SetIsOriginAllowed(origin => true)
.AllowCredentials());

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("The application has started.");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");

//app.UseWebSockets();

app.Run();