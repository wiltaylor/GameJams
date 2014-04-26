using UnityEngine;
using System.Collections;

public class DrillScript : MonoBehaviour
{



	// Use this for initialization
	void Start () {

	}

    void OnTriggerStay2D(Collider2D collider)
    {
        collider.gameObject.SendMessage("OnDrill");
    }
}
