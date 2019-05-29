﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Extensions;
using SIS.MvcFramework.Identity;
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

        protected Principal User => (Principal)this.Request.Session.GetParameter("principal");

        public IHttpRequest Request { get; set; }

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

        protected bool IsLoggedIn()
        {
            return this.User!=null;
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