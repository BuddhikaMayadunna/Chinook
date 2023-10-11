using Chinook.ClientModels;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ChinookContext DbContext;

        public ArtistService(ChinookContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<List<Artist>> GetArtistsAsync()
        {
            return await DbContext.Artists.ToListAsync();
        }

        public async Task<List<Album>> GetAlbumsForArtistAsync(int artistId)
        {
            return await DbContext.Albums.Where(a => a.ArtistId == artistId).ToListAsync();
        }

        /// <summary>
        /// Get the artist.
        /// </summary>
        /// <param name="artistId">The artist id</param>
        /// <returns>The artist.</returns>
        public async Task<Artist?> GetArtistAsync(long artistId)
        {
            return await DbContext.Artists.SingleOrDefaultAsync(a => a.ArtistId == artistId);
        }
    }
}
