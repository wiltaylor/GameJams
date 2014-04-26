using UnityEngine;
using System.Collections;

public class Drillable : MonoBehaviour {

    public float Health = 10f;
    public bool Invulnerable = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrill()
    {
        if (Invulnerable)
            return;

        Health -= 1f;

        if (Health < 0f)
        {
            Destroy(gameObject);
        }
    }
}
