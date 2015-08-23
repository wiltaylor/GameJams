using UnityEngine;
using System.Collections;

public class MeatFountainController : MonoBehaviour
{

    public float Duration = 1f;

	// Use this for initialization
	void Start () {
	    Destroy(gameObject, Duration);
	}
}
