using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.WebServer.Result
{
    public class FileResult : ActionResult
    {
        public FileResult(byte[] fileContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentLength, fileContent.Length.ToString()));
            this.Headers.AddHeader(new HttpHeader(HttpHeader.ContentDisposition, "attachment"));
            this.AddHeader(new HttpHeader(HttpHeader.ContentType, "application/json"));
            this.Content = fileContent;
        }
    }
}