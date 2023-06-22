using System.Text;

using Microservices.Gateway.Server.Models;

using Newtonsoft.Json;

namespace Microservices.Gateway.Server.Helpers;

public class HttpClientHelper(HttpClient httpClient) : IHttpClientHelper
{
  public async Task<HttpResponseData<T>> GetAsync<T>(string url)
  {
    var response = await httpClient.GetAsync(url);
    return await ProcessResponse<T>(response);
  }

  public async Task<HttpResponseData<TResponse>> PostAsync<TData, TResponse>(string url, TData? data)
  {
    var response = await httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
    return await ProcessResponse<TResponse>(response);
  }

  private async Task<HttpResponseData<T>> ProcessResponse<T>(HttpResponseMessage httpResponseMessage)
  {
    var content = await httpResponseMessage.Content.ReadAsStringAsync();
    if(typeof(T) == typeof(Guid))
    {
      return new HttpResponseData<T>
      {
        StatusCode = httpResponseMessage.StatusCode,
        Data = httpResponseMessage.IsSuccessStatusCode ? (T)(object)Guid.Parse(content.Replace("\"", "")) : default
      };
    }
    return new HttpResponseData<T>
    {
      StatusCode = httpResponseMessage.StatusCode,
      Data = httpResponseMessage.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync()) : default
    };
  }
}