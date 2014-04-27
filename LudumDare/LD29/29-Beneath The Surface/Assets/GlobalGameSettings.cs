using UnityEngine;
using System.Collections;

public class GlobalGameSettings : MonoBehaviour
{
    public static GlobalGameSettings Instance { get; private set; }

    public bool MusicEnabled = true;
    public bool SoundEnabled = true;

	// Use this for initialization
	void Awake () {
	    if (Instance != null)
	    {
	        Destroy(gameObject);
	        return;
	    }

	    DontDestroyOnLoad(gameObject);
	    Instance = this;
	}

}
