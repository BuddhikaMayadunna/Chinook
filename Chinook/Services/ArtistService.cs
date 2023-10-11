using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    /// <summary>
    /// The artist service.
    /// </summary>
    public class ArtistService : IArtistService
    {
        /// <summary>
        /// The db context.
        /// </summary>
        private readonly ChinookContext DbContext;

        /// <summary>
        /// The artist service constructor.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public ArtistService(ChinookContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Get the artist list.
        /// </summary>
        /// <returns>The artist list.</returns>
        public async Task<List<Artist>> GetArtistsAsync()
        {
            return await DbContext.Artists.ToListAsync();
        }

        /// <summary>
        /// Get the artist list.
        /// </summary>
        /// <param name="artistName">The artist name.</param>
        /// <returns>The artist list.</returns>
        public async Task<List<Artist>> GetArtistsAsync(string artistName)
        {
            return await DbContext.Artists.Where(w => w.Name.ToLower().Contains(artistName.ToLower())).ToListAsync();
        }

        /// <summary>
        /// Get The albums for the artist.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>The album list.</returns>
        public async Task<List<Album>> GetAlbumsForArtistAsync(int artistId)
        {
            return await DbContext.Albums.Where(a => a.ArtistId == artistId).ToListAsync();
        }

        /// <summary>
        /// Get the artist.
        /// </summary>
        /// <param name="artistId">The artist id</param>
        /// <returns>The artist.</returns>
        public async Task<Artist?> GetArtistsAsync(long artistId)
        {
            return await DbContext.Artists.SingleOrDefaultAsync(a => a.ArtistId == artistId);
        }


        public async Task CreatePlaylistAsync(ClientModels.Playlist playlist)
        {
            var tracks = new List<Track>();
            foreach (var track in playlist.Tracks)
            {
                var newTrack = new Track();
                newTrack.TrackId = track.TrackId;
                newTrack.Name = track.TrackName;

                var album = await DbContext.Albums.FirstOrDefaultAsync(p => p.Title == track.AlbumTitle);
                if (album != null)
                {
                    newTrack.AlbumId = album.AlbumId;
                }
                tracks.Add(newTrack);
            }
            var newPlayList = new Models.Playlist() { Name = playlist.Name, Tracks = tracks };
            var state = await DbContext.Playlists.AddAsync(newPlayList);
            await DbContext.SaveChangesAsync();
        }
    }
}
