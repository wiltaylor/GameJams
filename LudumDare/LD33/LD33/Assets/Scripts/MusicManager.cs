using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance;

	// Use this for initialization
	void Awake ()
    {
	    if (Instance != null)
	    {
	        Destroy(gameObject);
	        return;
	    }

	    Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
