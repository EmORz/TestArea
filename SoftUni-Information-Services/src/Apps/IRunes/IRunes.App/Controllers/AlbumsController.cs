using IRunes.App.Extensions;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.WebServer.Result;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        [Authorize()]
        public ActionResult All()
        {

            using (var context = new RunesDbContext())
            {
                ICollection<Album> allAlbums = context.Albums.ToList();

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
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [Authorize()]
        [HttpPost(ActionName = "Create")]
        public ActionResult CreateConfirm()
        {
            using (var context = new RunesDbContext())
            {
                string name = ((ISet<string>)Request.FormData["name"]).FirstOrDefault();
                string cover = ((ISet<string>)Request.FormData["cover"]).FirstOrDefault();

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

        [Authorize()]
        public ActionResult Details()
        {
           
            string albumId = Request.QueryData["id"].ToString();

            using (var context = new RunesDbContext())
            {
                Album albumFromDb = context.Albums
                    .Include(album => album.Tracks)
                    .SingleOrDefault(album => album.Id == albumId);

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
