using System;
using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform followObject;

	// Use this for initialization
	void Start ()
	{
        
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    try
	    {
            transform.position = new Vector3(followObject.position.x, followObject.position.y, -10);
	    }
	    catch (Exception)
	    {
	        //Do nothing player is destroyed.
	    }
        
	}
}
