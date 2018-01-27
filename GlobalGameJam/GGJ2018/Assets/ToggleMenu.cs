using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject MenuObject;
    public float TimeOut = 0.5f;

    private float _currentTimeout = 0f;

    void Start()
    {
        Cursor.visible = false;
    }

	void Update ()
	{
	    if (_currentTimeout >= 0f)
	        _currentTimeout -= Time.deltaTime;



	    if (Input.GetButton("Cancel") && _currentTimeout <= 0f)
	    {
	        MenuObject.SetActive(!MenuObject.activeInHierarchy);
	        _currentTimeout = TimeOut;

	        Cursor.visible = MenuObject.activeInHierarchy;


	    }
	}
}
