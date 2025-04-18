using System.Net;

namespace Reports.Api;

public class Response
{
    public HttpStatusCode RequestStatus { get; set; }
    public string Message { get; set; }
    public object Result { get; set; }
}
