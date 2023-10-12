using Chinook.ClientModels;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using Playlist = Chinook.ClientModels.Playlist;

namespace Chinook.Services
{
    /// <summary>
    /// The playlist service.
    /// </summary>
    public class PlaylistService : IPlaylistService
    {
        /// <summary>
        /// The db context.
        /// </summary>
        private readonly ChinookContext DbContext;

        /// <summary>
        /// The playlist service constructor.
        /// </summary>
        /// <param name="dbContext">The db context.</param>
        public PlaylistService(ChinookContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Get the playlist.
        /// </summary>
        /// <param name="currentUserId">The current user id.</param>
        /// <param name="playlistId">The play list id.</param>
        /// <returns>The playlist.</returns>
        public async Task<ClientModels.Playlist?> GetPlaylistAsync(string currentUserId, long playlistId)
        {
            var playlist = await DbContext.Playlists
            .Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist)
            .Where(p => p.PlaylistId == playlistId)
            .Select(p => new ClientModels.Playlist()
            {
                Name = p.Name,
                Tracks = p.Tracks.Select(t => new PlaylistTrack()
                {
                    AlbumTitle = t.Album.Title,
                    ArtistName = t.Album.Artist.Name,
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == currentUserId && up.Playlist.Name == "Favorites")).Any()
                }).ToList()
            })
            .FirstOrDefaultAsync();
            return playlist;
        }

        /// <summary>
        /// Get the playlists.
        /// </summary>
        /// <param name="currentUserId">The current user id.</param>
        /// <returns>The playlists.</returns>
        public async Task<List<Playlist>?> GetPlaylistsAsync(string currentUserId)
        {
            var playlists = await DbContext.Playlists.Select(p => new Playlist()
            {
                PlaylistId = p.PlaylistId,
                Name = p.Name
            }).ToListAsync();
            return playlists;
        }
    }
}
