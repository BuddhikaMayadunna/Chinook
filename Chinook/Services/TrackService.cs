using Chinook.ClientModels;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class TrackService : ITrackService
    {

        private List<PlaylistTrack>? Tracks;
        private readonly ChinookContext DbContext;

        public TrackService(ChinookContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public string FavoriteTrack(long trackId)
        {
            var track = Tracks?.FirstOrDefault(t => t.TrackId == trackId);
            return $"Track {track?.ArtistName} - {track?.AlbumTitle} - {track?.TrackName} added to playlist Favorites.";
        }

        /// <summary>
        /// Get play list tracks.
        /// </summary>
        /// <param name="currentUserId">The current user id.</param>
        /// <param name="artistId">The artist Id.</param>
        /// <returns>The playlist tracks.</returns>
        public List<PlaylistTrack> GetPlaylistTracks(string currentUserId, long artistId)
        {
            Tracks = DbContext.Tracks.Where(a => a.Album.ArtistId == artistId)
                     .Include(a => a.Album)
                     .Select(t => new PlaylistTrack()
                     {
                         AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                         TrackId = t.TrackId,
                         TrackName = t.Name,
                         IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == currentUserId && up.Playlist.Name == "Favorites")).Any()
                     })
                     .ToList();

            return Tracks;
        }

        public string UnfavoriteTrack(long trackId)
        {
            var track = Tracks?.FirstOrDefault(t => t.TrackId == trackId);
            return $"Track {track?.ArtistName} - {track?.AlbumTitle} - {track?.TrackName} removed from playlist Favorites.";
        }

        public PlaylistTrack? GetTrackById(long trackId)
        {
            return Tracks?.FirstOrDefault(t => t.TrackId == trackId);
        }

        public string AddTrackToPlaylist(Artist? artist, PlaylistTrack? selectedTrack)
        {
            if (artist is null)
                return $"Artist cannot be null";

            if (selectedTrack is null)
                return $"Select track cannot be null";

            var data  = DbContext.Tracks.Where(a => a.Album.ArtistId == artist.ArtistId);
            //.Playlists.AddAsync();
            return $"Track {artist.Name} - {selectedTrack.AlbumTitle} - {selectedTrack.TrackName} added to playlist {{playlist name}}.";
        }
    }
}
