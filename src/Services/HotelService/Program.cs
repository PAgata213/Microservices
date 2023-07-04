using HotelService.DBContext;
using HotelService.Repository;
using HotelService.WebAPI;

using MassTransit;

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

builder.Services.AddMassTransit(cfg =>
{
  cfg.AddBus(busCtx => Bus.Factory.CreateUsingRabbitMq(rmcfg =>
  {
    rmcfg.Host("rabbitmq-node", "/", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands:CreateHotelReservation", e =>
    {
      e.ConfigureConsumer<CreateHotelReservationConsumer>(busCtx);
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands:CancelHotelReservation", e =>
    {
      e.ConfigureConsumer<CancelHotelReservationConsumer>(busCtx);
    });
  }));
  cfg.AddConsumer<CreateHotelReservationConsumer>();
  cfg.AddConsumer<CancelHotelReservationConsumer>();
});

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

//app.UseHttpsRedirection();

app.Run();
