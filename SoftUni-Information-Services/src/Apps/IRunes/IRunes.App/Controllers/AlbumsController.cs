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

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService; 
        public AlbumsController()
        {
            this.albumService = new AlbumService();
        }
        [Authorize()]
        public ActionResult All()
        {

                ICollection<Album> allAlbums = this.albumService.GetAllAlbums();

                if (allAlbums.Count == 0)
                {
                    this.ViewData["Albums"] = "There are currently no albums.";
                }
                else
                {
                    this.ViewData["Albums"] =
                        string.Join(string.Empty,
                        allAlbums.Select(album => album.ToHtmlAll()).ToList());
                }

                return this.View();
            
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
