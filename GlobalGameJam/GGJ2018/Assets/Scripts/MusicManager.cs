using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource ExtraDrumTrack;

    public float MaxWait;
    public float MinWait;

    private float _currentWait;
    private bool _waitingForPlay;

    void Start()
    {
        _currentWait = Random.Range(MinWait, MaxWait);
        _waitingForPlay = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (_currentWait > 0f)
        {
            _currentWait -= Time.deltaTime;
            return;
        }

        if (!ExtraDrumTrack.isPlaying && !_waitingForPlay)
        {
            _currentWait = Random.Range(MinWait, MaxWait);
            _waitingForPlay = true;
            return;
        }

        if (_waitingForPlay)
        {
            ExtraDrumTrack.Play();
            _waitingForPlay = false;
        }

	}
}
