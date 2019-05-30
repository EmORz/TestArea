using System.Collections.Generic;
using System.Linq;
using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
<<<<<<< HEAD
using IRunes.Services;
=======
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
<<<<<<< HEAD
using SIS.MvcFramework.Attributes.Security;
using SIS.WebServer.Result;
=======
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
<<<<<<< HEAD
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
=======
        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["albumId"].ToString();
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37

            this.ViewData["AlbumId"] = albumId;
            return this.View();
        }

<<<<<<< HEAD
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

=======
        [HttpPost(ActionName = "Create")]
        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["albumId"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums.SingleOrDefault(album => album.Id == albumId);

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                string name = ((ISet<string>)httpRequest.FormData["name"]).FirstOrDefault();
                string link = ((ISet<string>)httpRequest.FormData["link"]).FirstOrDefault();
                string price = ((ISet<string>)httpRequest.FormData["price"]).FirstOrDefault();

                Track trackForDb = new Track
                {
                    Name = name,
                    Link = link,
                    Price = decimal.Parse(price)
                };

                albumFromDb.Tracks.Add(trackForDb);
                albumFromDb.Price = (albumFromDb.Tracks
                                         .Select(track => track.Price)
                                         .Sum() * 87) / 100;
                context.Update(albumFromDb);
                context.SaveChanges();
            }

            return this.Redirect($"/Albums/Details?id={albumId}");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["albumId"].ToString();
            string trackId = httpRequest.QueryData["trackId"].ToString();

            using (var context = new RunesDbContext())
            {
                Track trackFromDb = context.Tracks.SingleOrDefault(track => track.Id == trackId);

                if (trackFromDb == null)
                {
                    return this.Redirect($"/Albums/Details?id={albumId}");
                }

                this.ViewData["AlbumId"] = albumId;
                this.ViewData["Track"] = trackFromDb.ToHtmlDetails(albumId);
                return this.View();
            }
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
        }
    }
}
