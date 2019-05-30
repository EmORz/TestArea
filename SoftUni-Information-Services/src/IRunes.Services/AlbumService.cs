using System.Collections.Generic;
using System.Linq;
using IRunes.Data;
using IRunes.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunes.Services
{
    public class AlbumService : IAlbumService
    {
        private RunesDbContext context;
        public AlbumService()
        {
            this.context = new RunesDbContext();
        }
        public Album CreateAlbum(Album album)
        {
                album = context.Albums.Add(album).Entity;
                context.SaveChanges();
                return album;
        }

        public bool AddTrackToAlbum(string albumId, Track trackForDB)
        {
            Album albumFromDb = this.GetAllAlbumById(albumId);

            albumFromDb.Tracks.Add(trackForDB);
            albumFromDb.Price = (albumFromDb.Tracks
                                     .Select(track => track.Price)
                                     .Sum() * 87) / 100;
            this.context.Update(albumFromDb);
            this.context.SaveChanges();
            return true;
        }

        public ICollection<Album> GetAllAlbums()
        {
            return context.Albums.ToList();
        }

        public Album GetAllAlbumById(string id)
        {
            return context.Albums
                .Include(album => album.Tracks)
                .SingleOrDefault(x => x.Id == id);
        }
    }
}