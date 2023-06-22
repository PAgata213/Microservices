using Microservices.Gateway.Server.Models;

namespace Microservices.Gateway.Server.Helpers;

public interface IHttpClientHelper
{
  Task<HttpResponseData<T>> GetAsync<T>(string url);
  Task<HttpResponseData<TResponse>> PostAsync<TData, TResponse>(string url, TData? data);
}
