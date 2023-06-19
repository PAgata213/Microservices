using HotelService.DBContext;
using HotelService.Repository;
using HotelService.WebAPI;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HotelDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("HotelDb"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IHotelRepository, HotelRepository>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
  dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapHotelEndpoints();

app.UseHttpsRedirection();

app.Run();