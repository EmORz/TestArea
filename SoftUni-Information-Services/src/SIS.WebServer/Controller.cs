using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Extensions;
<<<<<<< HEAD
using SIS.MvcFramework.Identity;
=======
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
using SIS.MvcFramework.Result;
using SIS.WebServer.Result;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected Controller()
        {
            ViewData = new Dictionary<string, object>();
        }

<<<<<<< HEAD
        public Principal User => 
            this.Request.Session.ContainsParameter("principal")
            ?(Principal)this.Request.Session.GetParameter("principal")
            :null;

        public IHttpRequest Request { get; set; }

=======
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
        protected Dictionary<string, object> ViewData;

        private string ParseTemplate(string viewContent)
        {
            foreach (var param in ViewData)
            {
                viewContent = viewContent.Replace($"@Model.{param.Key}",
                    param.Value.ToString());
            }

            return viewContent;
        }

<<<<<<< HEAD
        protected bool IsLoggedIn()
        {
            return this.Request.Session.ContainsParameter("principal");
        }

        protected void SignIn( string id, string username, string email)
        {
            this.Request.Session.AddParameter("principal", new Principal
            {
                Id = id,
                Email =  email,
                Username = username
            });
            //Request.Session.AddParameter("id", id);
            //Request.Session.AddParameter("username", username);
            //Request.Session.AddParameter("email", email);
        }

        protected void SignOut()
        {
            Request.Session.ClearParameters();
=======
        protected bool IsLoggedIn(IHttpRequest request)
        {
            return request.Session.ContainsParameter("username");
        }

        protected void SignIn(IHttpRequest httpRequest, string id, string username, string email)
        {
            httpRequest.Session.AddParameter("id", id);
            httpRequest.Session.AddParameter("username", username);
            httpRequest.Session.AddParameter("email", email);
        }

        protected void SignOut(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
        }

        protected ActionResult View([CallerMemberName] string view = null)
        {
            string controllerName = GetType().Name.Replace("Controller", string.Empty);
            string viewName = view;

            string viewContent = System.IO.File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");

            viewContent = ParseTemplate(viewContent);

            HtmlResult htmlResult = new HtmlResult(viewContent);

            return htmlResult;
        }

        protected ActionResult Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected ActionResult Xml(object obj)
        {
            return new XmlResult(obj.ToXml());
        }

        protected ActionResult Json(object obj)
        {

            return new JsonResult(obj.ToJson());
        }

        protected ActionResult File(byte[] fileContent)
        {

            return new FileResult(fileContent);
        }

        protected ActionResult NotFound(string message = "")
        {
            return new NotFoundResult(message);
        }
    }
}
