using System;
using System.Linq;
using UnityEngine;

namespace Assets.Systems.Music
{
    public enum SfxType
    {
        Attack,
        BuildUnit,
        DemonAttack,
        Mine
    }

    public class MusicService
    {
        private static MusicService _instance;
        private MusicPlaylist[] _playlists;
        private MusicPlaylist _currentPlaylist;
        private int _currentTrack;
        private SfxList _sfx;

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

        public void AssignPlaylists(MusicPlaylist[] playlists, SfxList sfx)
        {
            _playlists = playlists;
            _currentPlaylist = _playlists.First();

            _sfx = sfx;
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

        public void PlaySfx(SfxType type)
        {
            if (_sfx == null)
                return;

            switch (type)
            {
                case SfxType.Attack:
                    AudioSource.PlayClipAtPoint(_sfx.Attack, Vector3.zero);
                    break;
                case SfxType.BuildUnit:
                    AudioSource.PlayClipAtPoint(_sfx.BuildUnit, Vector3.zero);
                    break;
                case SfxType.DemonAttack:
                    AudioSource.PlayClipAtPoint(_sfx.DemonAttack, Vector3.zero);
                    break;
                case SfxType.Mine:
                    AudioSource.PlayClipAtPoint(_sfx.Mine, Vector3.zero);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }
    }
}
