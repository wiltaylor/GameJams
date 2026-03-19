using UnityEngine;
using System.Collections;

public class GameMan : MonoBehaviour
{
    public static GameMan Instance { get; private set; }
    public string LastRace = "None";
    public int LastPlayerRank = 0;

	void Awake ()
	{
	    if (Instance == null)
	        Instance = this;
	    else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

    public void EndRace()
    {
        Application.LoadLevel("RaceOver");
    }
}
