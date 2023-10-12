using Chinook.ClientModels;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    /// <summary>
    /// The track service.
    /// </summary>
    public class TrackService : ITrackService
    {
        /// <summary>
        /// The playlist tracks list.
        /// </summary>
        private List<PlaylistTrack>? Tracks;

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly ChinookContext DbContext;

        /// <summary>
        /// The track service.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public TrackService(ChinookContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Set the favorite track.
        /// </summary>
        /// <param name="trackId">The track id.</param>
        /// <returns>The status.</returns>
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

        /// <summary>
        /// Set the unfavorite track.
        /// </summary>
        /// <param name="trackId">The track id.</param>
        /// <returns>The status.</returns>
        public string UnfavoriteTrack(long trackId)
        {
            var track = Tracks?.FirstOrDefault(t => t.TrackId == trackId);
            return $"Track {track?.ArtistName} - {track?.AlbumTitle} - {track?.TrackName} removed from playlist Favorites.";
        }

        /// <summary>
        /// Get the track by id.
        /// </summary>
        /// <param name="trackId">The track id.</param>
        /// <returns>The playlist tracks.</returns>
        public PlaylistTrack? GetTrackById(long trackId)
        {
            return Tracks?.FirstOrDefault(t => t.TrackId == trackId);
        }

        /// <summary>
        /// Add tracks to playlist.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="selectedTrack">The selected track.</param>
        /// <param name="newPlaylist">the new playlist.</param>
        /// <returns>The status.</returns>
        public async Task<string> AddTrackToPlaylist(Artist? artist, PlaylistTrack? selectedTrack, string newPlaylist)
        {
            if (artist is null)
                return $"Artist cannot be null";

            if (selectedTrack is null)
                return $"Select track cannot be null";

            var tracks = new List<Track>();
          
            var newTrack = new Track();
            newTrack.TrackId = selectedTrack.TrackId;
            newTrack.Name = selectedTrack.TrackName;
            newTrack.UnitPrice = Array.Empty<byte>();

            var album = await DbContext.Albums.FirstOrDefaultAsync(p => p.Title == selectedTrack.AlbumTitle);
            if (album != null)
            {
                newTrack.AlbumId = album.AlbumId;
            }
           // tracks.Add(newTrack);

            var newPlayList = new Models.Playlist()
            {
                Name = newPlaylist,
                //need to check the unique constraint
                //Tracks = new List<Track> { newTrack }
            };
            var state = await DbContext.Playlists.AddAsync(newPlayList);
            await DbContext.SaveChangesAsync();

            return $"Track {artist.Name} - {selectedTrack.AlbumTitle} - {selectedTrack.TrackName} added to playlist " +
                $"{newPlaylist}.";
        }
    }
}
