using ApiMicrosservicesProduct.Errors.Interfaces;
using System.Net;

namespace ApiMicrosservicesProduct.Errors;
public class RequestError : IRequestError
{
    public string Name { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool Response { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}