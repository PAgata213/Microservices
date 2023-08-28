using Chronicle;

using MassTransit;

using Microservices.Gateway.Server;
using Microservices.Gateway.Server.Helpers;
using Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga;
using Microservices.Gateway.Server.Services;
using Microservices.Gateway.Server.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IReservationService, ReservationService>();
builder.Services.AddChronicle();
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
    rmcfg.ReceiveEndpoint("Microservices.Gateway.ConsumerSaga", e =>
    {
      e.ConfigureSaga<CreateReservationWithConsumerSaga>(busCtx);
    });
  });
  cfg.AddSaga<CreateReservationWithConsumerSaga>()
    .InMemoryRepository();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);
//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapReservationEndpoints();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
