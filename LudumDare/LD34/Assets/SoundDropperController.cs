using UnityEngine;
using System.Collections;

public class SoundDropperController : MonoBehaviour
{

    private AudioSource _audio;

    void OnEnable()
    {
        if(_audio == null)
            _audio = GetComponent<AudioSource>();

        if (_audio.clip != null)
            _audio.Play();
    }

    void Update()
    {
        if (!_audio.isPlaying)
        {
            _audio.clip = null;
            gameObject.SetActive(false);
        }
    }


	
}
