using UnityEngine;
using System.Collections;

public class RaceOverGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(0,0,100,100), "You have finished the race. You came " + GameMan.Instance.LastPlayerRank);
    }
}
