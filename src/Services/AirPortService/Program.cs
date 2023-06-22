using AirPortService.DBContext;
using AirPortService.Repository;
using AirPortService.WebAPI;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AirPortDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("AirPortDb"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IAirPortRepository, AirPortRepository>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<AirPortDbContext>();
  dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapAirPortEndpoints();

//app.UseHttpsRedirection();

app.Run();