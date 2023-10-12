using Chinook.Models;

namespace Chinook.Services
{
    /// <summary>
    /// The artist service interface.
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        /// Get the artist list.
        /// </summary>
        /// <returns>The artist list.</returns>
        Task<List<Artist>> GetArtistsAsync();
        /// <summary>
        /// Get the artist.
        /// </summary>
        /// <param name="artistId">The artist id</param>
        /// <returns>The artist.</returns>
        Task<Artist?> GetArtistsAsync(long artistId);

        /// <summary>
        /// Get The albums for the artist.
        /// </summary>
        /// <param name="artistId">The artist id.</param>
        /// <returns>The album list.</returns>
        Task<List<Album>> GetAlbumsForArtistAsync(int artistId);

        /// <summary>
        /// Get the artist list.
        /// </summary>
        /// <param name="artistName">The artist name.</param>
        /// <returns>The artist list.</returns>
        Task<List<Artist>> GetArtistsAsync(string artistName);
    }
}
