using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.Services
{
    public interface IArtistService
    {
        Task<Artist?> GetArtistAsync(long artistId);
        Task<List<Album>> GetAlbumsForArtistAsync(int artistId);
        Task<List<Artist>> GetArtistsAsync();
    }
}
