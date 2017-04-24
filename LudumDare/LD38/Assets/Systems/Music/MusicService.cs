using System.Linq;
using UnityEngine;

namespace Assets.Systems.Music
{
    public class MusicService
    {
        private static MusicService _instance;
        private MusicPlaylist[] _playlists;
        private MusicPlaylist _currentPlaylist;
        private int _currentTrack;

        public static MusicService Instance
        {
            get { return _instance ?? (_instance = new MusicService()); }
        }

        public void SetPlaylist(string name)
        {
            if (_playlists == null)
                return;

            _currentPlaylist = _playlists.First(p => p.name == name);
            _currentTrack = 0;
        }

        public void AssignPlaylists(MusicPlaylist[] playlists)
        {
            _playlists = playlists;
            _currentPlaylist = _playlists.First();
        }

        public AudioClip CurrentTrack
        {
            get
            {
                if (_currentTrack == -1)
                    return null;

                return _currentPlaylist == null ? null : 
                    _currentPlaylist.Track[_currentTrack];
            }
        }

        public void Next()
        {
            _currentTrack++;

            if (_currentTrack >= _currentPlaylist.Track.Length)
                _currentTrack = 0;
        }
    }
}
