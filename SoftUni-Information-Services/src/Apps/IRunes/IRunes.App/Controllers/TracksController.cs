using System.Collections.Generic;
using System.Linq;
using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using IRunes.Services;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.WebServer.Result;

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITrackService trackService;

        private readonly IAlbumService albumService;

        public TracksController()
        {
            this.trackService = new TrackService();
            this.albumService = new AlbumService();
        }
        [Authorize()]
        public ActionResult Create()
        {
            string albumId = Request.QueryData["albumId"].ToString();

            this.ViewData["AlbumId"] = albumId;
            return this.View();
        }

        [Authorize()]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            string albumId = Request.QueryData["albumId"].ToString();


            //if (albumFromDb == null)
            //{
            //    return this.Redirect("/Albums/All");
            //}

            string name = ((ISet<string>)Request.FormData["name"]).FirstOrDefault();
            string link = ((ISet<string>)Request.FormData["link"]).FirstOrDefault();
            string price = ((ISet<string>)Request.FormData["price"]).FirstOrDefault();

            Track trackForDb = new Track
            {
                Name = name,
                Link = link,
                Price = decimal.Parse(price)
            };

            if (!this.albumService.AddTrackToAlbum(albumId, trackForDb))
            {
                return this.Redirect("/Albums/All");
            }
            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        [Authorize()]
        public ActionResult Details()
        {
            string albumId = Request.QueryData["albumId"].ToString();
            string trackId = Request.QueryData["trackId"].ToString();

            Track trackFromDb = this.trackService.GetTrackById(trackId);

            if (trackFromDb == null)
            {
                return this.Redirect($"/Albums/Details?id={albumId}");
            }

            this.ViewData["AlbumId"] = albumId;
            this.ViewData["Track"] = trackFromDb.ToHtmlDetails(albumId);
            return this.View();

        }
    }
}
