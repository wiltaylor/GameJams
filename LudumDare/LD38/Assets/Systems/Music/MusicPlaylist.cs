using UnityEngine;

namespace Assets.Systems.Music
{
    [CreateAssetMenu(fileName = "Playlist", menuName = "Music/Playlist", order = 1)]
    public class MusicPlaylist : ScriptableObject
    {
        public string Name;
        public AudioClip[] Track;
    }
}
