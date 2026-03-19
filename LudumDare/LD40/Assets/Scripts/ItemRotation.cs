using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{

    public float RotationSpeed = 1f;
	
	// Update is called once per frame
	void Update ()
	{
	    transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
	}
}
