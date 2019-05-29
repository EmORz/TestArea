using System.Net;
using SIS.HTTP.Enums;
using SIS.HTTP.Responses;

namespace SIS.WebServer.Result
{
    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(HttpResponseStatusCode httpResponseStatusCode) : base(httpResponseStatusCode)
        {
            
        }
        
    }
}