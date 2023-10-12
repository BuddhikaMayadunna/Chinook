namespace Chinook.Services
{
    /// <summary>
    /// The playlist interface.
    /// </summary>
    public interface IPlaylistService
    {
        /// <summary>
        /// Get the playlist.
        /// </summary>
        /// <param name="currentUserId">The current user id.</param>
        /// <param name="playlistId">The play list id.</param>
        /// <returns>The playlist.</returns>
        Task<ClientModels.Playlist> GetPlaylistAsync(string currentUserId, long playlistId);

        /// <summary>
        /// Get the playlists.
        /// </summary>
        /// <param name="currentUserId">The current user id.</param>
        /// <returns>The playlists.</returns>
        Task<List<ClientModels.Playlist>?> GetPlaylistsAsync(string currentUserId);

        /// <summary>
        /// Create playlist.
        /// </summary>
        /// <param name="playlist">The playlist</param>
        /// <returns>The create playlist status.</returns>
        //Task CreatePlaylistAsync(ClientModels.Playlist playlist);
    }
}