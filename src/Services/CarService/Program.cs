using CarService.DBContext;
using CarService.Repository;
using CarService.WebAPI;

using MassTransit;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("CarDb"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddMassTransit(cfg =>
{
  cfg.AddBus(busCtx => Bus.Factory.CreateUsingRabbitMq(rmcfg =>
  {
    rmcfg.Host("rabbitmq-node", "/", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands:CreateCarReservation", e =>
    {
      e.ConfigureConsumer<CreateCarReservationConsumer>(busCtx);
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands:CancelCarReservation", e =>
    {
      e.ConfigureConsumer<CancelCarReservationConsumer>(busCtx);
    });
  }));
  cfg.AddConsumer<CreateCarReservationConsumer>();
  cfg.AddConsumer<CancelCarReservationConsumer>();
});

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<CarDbContext>();
  dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapCarEndpoints();

//app.UseHttpsRedirection();

app.Run();
