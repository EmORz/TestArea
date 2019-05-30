<<<<<<< HEAD
﻿using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.WebServer.Result;
using System.Collections.Generic;
using System.Linq;
using IRunes.Services;
=======
﻿using System.Collections.Generic;
using System.Linq;
using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
<<<<<<< HEAD
        private readonly IAlbumService albumService; 
        public AlbumsController()
        {
            this.albumService = new AlbumService();
        }
        [Authorize()]
        public ActionResult All()
        {

                ICollection<Album> allAlbums = this.albumService.GetAllAlbums();
=======
        public IHttpResponse All(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                ICollection<Album> allAlbums = context.Albums.ToList();
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37

                if (allAlbums.Count == 0)
                {
                    this.ViewData["Albums"] = "There are currently no albums.";
                }
                else
                {
                    this.ViewData["Albums"] =
<<<<<<< HEAD
                        string.Join(string.Empty,
=======
                        string.Join(string.Empty, 
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
                        allAlbums.Select(album => album.ToHtmlAll()).ToList());
                }

                return this.View();
<<<<<<< HEAD
            
        }

        [Authorize()]
        public ActionResult Create()
        {
            return this.View();
        }

        [Authorize()]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            string name = ((ISet<string>)Request.FormData["name"]).FirstOrDefault();
            string cover = ((ISet<string>)Request.FormData["cover"]).FirstOrDefault();

            Album album = new Album
            {
                Name = name,
                Cover = cover,
                Price = 0M
            };
            this.albumService.CreateAlbum(album);
            return this.Redirect("/Albums/All");
        }

        [Authorize()]
        public ActionResult Details()
        {

            string albumId = Request.QueryData["id"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = this.albumService.GetAllAlbumById(albumId);
=======
            }
        }

        public IHttpResponse Create(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost(ActionName = "Create")]
        public IHttpResponse CreateConfirm(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            using (var context = new RunesDbContext())
            {
                string name = ((ISet<string>)httpRequest.FormData["name"]).FirstOrDefault();
                string cover = ((ISet<string>)httpRequest.FormData["cover"]).FirstOrDefault();

                Album album = new Album
                {
                    Name = name,
                    Cover = cover,
                    Price = 0M
                };

                context.Albums.Add(album);
                context.SaveChanges();
            }

            return this.Redirect("/Albums/All");
        }

        public IHttpResponse Details(IHttpRequest httpRequest)
        {
            if (!this.IsLoggedIn(httpRequest))
            {
                return this.Redirect("/Users/Login");
            }

            string albumId = httpRequest.QueryData["id"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums
                    .Include(album => album.Tracks)
                    .SingleOrDefault(album => album.Id == albumId);
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37

                if (albumFromDb == null)
                {
                    return this.Redirect("/Albums/All");
                }

                this.ViewData["Album"] = albumFromDb.ToHtmlDetails();
                return this.View();
            }
        }
    }
}
