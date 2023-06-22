using System.Net;

namespace Microservices.Gateway.Server.Models;

public class HttpResponseData<T>
{
  public T? Data { get; init; }
  public HttpStatusCode StatusCode { get; init; }
  public bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode < 300;

}
