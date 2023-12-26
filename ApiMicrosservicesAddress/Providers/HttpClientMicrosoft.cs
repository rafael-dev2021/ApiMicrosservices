using ApiMicrosservicesAddress.Errors;
using ApiMicrosservicesAddress.Providers.Interfaces;
using System.Net;

namespace ApiMicrosservicesAddress.Providers;

public class HttpClientMicrosoft : IHttpClient
{
    private readonly HttpClient _httpClient;

    public HttpClientMicrosoft()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://viacep.com.br/ws/")
        };
    }

    public async Task<string> GetCepAsync(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{url}/json/").ConfigureAwait(false);
            if (response == null || !response.IsSuccessStatusCode)
            {
                // Handle "Resource not found" error
                throw new RequestException(new RequestError
                {
                    Message = "Cep não encontrado!",
                    Severity = "error",
                    StatusCode = HttpStatusCode.NotFound
                });
            }
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            // Handle a request-level exception, such as network issues
            throw new RequestException(new RequestError
            {
                Message = "Erro na requisição: " + ex.Message,
                Severity = "error",
                StatusCode = HttpStatusCode.InternalServerError
            });
        }
    }
}
