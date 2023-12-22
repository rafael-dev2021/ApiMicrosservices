using ApiMicrosservicesProduct.Errors.Interfaces;
using System.Net;

namespace ApiMicrosservicesProduct.Errors;
public class RequestException(IRequestError ex) : Exception, IRequestException
{
    public string Name { get; set; } = "Error!";
    public string Severity { get; set; } = ex.Severity;
    public override string Message { get; } = ex.Message!;
    public bool Response { get; set; } = true;
    public HttpStatusCode StatusCode { get; set; } = ex.StatusCode;
}