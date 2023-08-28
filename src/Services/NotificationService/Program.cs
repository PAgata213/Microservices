using MassTransit;

using NotificationService;
using NotificationService.Handlers;
using NotificationService.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<INotificationHubHelper, NotificationHubHelper>();

builder.Services.AddCors();

builder.Services.AddMassTransit(cfg =>
{
  cfg.UsingRabbitMq((busCtx, rmcfg) =>
  {
    rmcfg.Host("rabbitmq-node", "/", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Shared.ConsumerSaga.Events:ReservationCreatedConsumer", e =>
    {
      e.ConfigureConsumer<ReservationCreatedConsumer>(busCtx);
    });
    rmcfg.ReceiveEndpoint("Microservices.Gateway.Shared.ConsumerSaga.Events:ReservationFailedConsumer", e =>
    {
      e.ConfigureConsumer<ReservationFailedConsumer>(busCtx);
    });
  });
  cfg.AddConsumer<ReservationCreatedConsumer>();
  cfg.AddConsumer<ReservationFailedConsumer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);

app.MapHub<NotificationHub>("/chathub");
app.Run();