using AirPortService.DBContext;
using AirPortService.Repository;
using AirPortService.WebAPI;

using MassTransit;

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

builder.Services.AddMassTransit(cfg =>
{
  cfg.UsingRabbitMq((busCtx, rmcfg) =>
  {
    rmcfg.Host("rabbitmq-node", "/", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands:CreateFlyReservation", e =>
    {
      e.ConfigureConsumer<CreateFlyReservationConsumer>(busCtx);
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands:CancelFlyReservation", e =>
    {
      e.ConfigureConsumer<CancelFlyReservationConsumer>(busCtx);
    });
  });

  cfg.AddConsumer<CreateFlyReservationConsumer>();
  cfg.AddConsumer<CancelFlyReservationConsumer>();
});

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