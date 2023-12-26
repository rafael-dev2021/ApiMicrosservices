﻿using System.Net;

namespace ApiMicrosservicesAddress.Errors.Interfaces;
public interface IRequestException
{
    string Name { get; set; }
    string Severity { get; set; }
    string Message { get; }
    bool Response { get; set; }
    HttpStatusCode? StatusCode { get; set; }
}
