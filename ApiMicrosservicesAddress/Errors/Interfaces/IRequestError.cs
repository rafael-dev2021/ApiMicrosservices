﻿using System.Net;

namespace ApiMicrosservicesAddress.Errors.Interfaces;
public interface IRequestError
{
    string Name { get; set; }
    string Severity { get; set; }
    string Message { get; set; }
    bool Response { get; set; }
    HttpStatusCode? StatusCode { get; set; }
}
