using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.Services
{
    public interface ITrackService
    {
        string FavoriteTrack(long trackId);
        string UnfavoriteTrack(long trackId);
        List<PlaylistTrack> GetPlaylistTracks(string currentUserId, long artistId);
        PlaylistTrack? GetTrackById(long trackId);
        string AddTrackToPlaylist(Artist? artist, PlaylistTrack? selectedTrack);

    }
}
