namespace ApiMicrosservicesAddress.Providers.Interfaces;

public interface IHttpClient
{
    Task<string> GetCepAsync(string url);
}
