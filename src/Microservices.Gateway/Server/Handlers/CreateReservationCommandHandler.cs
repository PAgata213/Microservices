using System.Net.Http;

using MediatR;

using Microservices.Gateway.Server.Commands;

namespace Microservices.Gateway.Server.Handlers;

public class CreateReservationCommandHandler(HttpClient httpClient) : IRequestHandler<CreateReservationCommand>
{
  public Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}
