using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRedirect : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Invoke("Redirect", 1f);
	}

    private void Redirect()
    {
        SceneManager.LoadScene("MainGame");
    }
	
}
