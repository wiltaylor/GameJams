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
        transform.position = new Vector3(followObject.position.x, followObject.position.y, -10);
	}
}
