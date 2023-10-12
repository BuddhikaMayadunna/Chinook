using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.Services
{
    /// <summary>
    /// The track service interface.
    /// </summary>
    public interface ITrackService
    {
        /// <summary>
        /// Set the favorite track. 
        /// </summary>
        /// <param name="trackId">The tack id.</param>
        /// <returns>The favorite tack.</returns>
        string FavoriteTrack(long trackId);

        /// <summary>
        /// Set the unfavorite track.
        /// </summary>
        /// <param name="trackId">The tack id.</param>
        /// <returns>The favorite tack.</returns>
        string UnfavoriteTrack(long trackId);

        /// <summary>
        /// Get the playlist traks. 
        /// </summary>
        /// <param name="currentUserId">The current user id</param>
        /// <param name="artistId">The artist id.</param>
        /// <returns>The playlist traks.</returns>
        List<PlaylistTrack> GetPlaylistTracks(string currentUserId, long artistId);

        /// <summary>
        /// Get the track by id.
        /// </summary>
        /// <param name="trackId">The track id.</param>
        /// <returns>The traks.</returns>
        PlaylistTrack? GetTrackById(long trackId);

        /// <summary>
        /// Add tracks to playlist.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="selectedTrack">The selected track.</param>
        /// <param name="newPlaylist">the new playlist.</param>
        /// <returns>The status.</returns>
        Task<string> AddTrackToPlaylist(Artist? artist, PlaylistTrack? selectedTrack, string newPlaylist);

    }
}
