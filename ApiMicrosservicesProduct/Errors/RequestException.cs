using ApiMicrosservicesProduct.Errors.Interfaces;
using System.Net;

namespace ApiMicrosservicesProduct.Errors;

public class RequestException(IRequestError ex) : Exception, IRequestException
{
    public string Name { get; set; } = "Error!";
    public override string Message { get; } = ex.Message;
    public string Severity { get; set; } = ex.Severity;
    public bool Response { get; set; } = true;
    public HttpStatusCode HttpStatus { get; set; } = ex.HttpStatus;
}
