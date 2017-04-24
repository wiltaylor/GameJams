using Assets.Systems.Music;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource AudioSource;
    public MusicPlaylist[] Playlists;
	
	private void Start ()
    {
		DontDestroyOnLoad(gameObject);
        MusicService.Instance.AssignPlaylists(Playlists);

    }
	
	private void Update ()
    {
        if (MusicService.Instance.CurrentTrack == null)
        {
            AudioSource.Stop();
            return;
        }

        if (MusicService.Instance.CurrentTrack != AudioSource.clip)
        {
            AudioSource.clip = MusicService.Instance.CurrentTrack;
            AudioSource.Play();
        }

        if (!AudioSource.isPlaying)
        {
            MusicService.Instance.Next();
        }
	}
}
