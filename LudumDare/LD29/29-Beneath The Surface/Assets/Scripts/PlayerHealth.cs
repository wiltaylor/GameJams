using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float Health = 20f;
    public float MaxHealth = 20f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Health < 0f)
	    {
	        Destroy(gameObject);
	    }
	}

    void Damage(float ammout)
    {
        Health -= ammout;
    }
}
