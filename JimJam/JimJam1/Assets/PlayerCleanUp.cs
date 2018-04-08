using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCleanUp : MonoBehaviour {

	void Start ()
    {
        if(PlayerController.Instance != null)
		Destroy(PlayerController.Instance);
    }
	

}
