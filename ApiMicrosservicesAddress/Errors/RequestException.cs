using ApiMicrosservicesAddress.Errors.Interfaces;
using System.Net;

namespace ApiMicrosservicesAddress.Errors;
public class RequestException(IRequestError ex) : Exception, IRequestException
{
    public string Name { get; set; } = "Erro!";
    public string Severity { get; set; } = ex.Severity;
    public bool Response { get; set; } = true;
    public HttpStatusCode? StatusCode { get; set; } = ex.StatusCode;
    public override string Message { get; } = ex.Message!;
}
