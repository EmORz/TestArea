using System.Net;
using System.Reflection;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes.Action;
using SIS.WebServer.Result;

namespace IRunes.App.Controllers
{
    public class InfoController : Controller
    {

        public int MyProperty { get; set; }

        [NonAction]
        public override string ToString()
        {
            return base.ToString();
        }

<<<<<<< HEAD
        public ActionResult Json(IHttpRequest request)
        {
            return Json(new
            {
                Name = "Pesho",
                Age = 25,
                Occupation = "Free Lancer",
                Married = false
            });

        }


        public ActionResult File(IHttpRequest request)
        {
=======
        public ActionResult File(IHttpRequest request)
        {
            //var path = request.Path;
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
            string folderPrefix = "/../";
            string assemblyLocation = this.GetType().Assembly.Location;
            string resourceFolderPath = "Resources/";
            string requestedResource = request.QueryData["file"].ToString();

            string fullPathToResource = assemblyLocation + folderPrefix + resourceFolderPath + requestedResource;

            if (System.IO.File.Exists(fullPathToResource))
            {
                byte[] content = System.IO.File.ReadAllBytes(fullPathToResource);
                return File(content);
            }

            return NotFound();
        }
        public IHttpResponse About(IHttpRequest request)
        {
            return this.View();
        }
    }
}
