using UnityEngine;
using System.Collections;

public class DirtController : MonoBehaviour {

    public float TimeToDie = 3f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, TimeToDie);
	}
	
}
