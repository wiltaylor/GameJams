using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{

    public float Lower;
    public float Upper;
    public float Speed;
    public bool Moving;
    public bool Upwards;
    public AudioSource LiftSound;

    public void ActivateLift()
    {
        if (Moving)
            return;

        Upwards = transform.position.y - Lower < Upper - transform.position.y;
        Moving = true;

        LiftSound.Play();
    }
    
	void Update ()
	{
	    if (!Moving) return;

	    if (Upwards)
	    {
	        var newVec = new Vector3(transform.position.x, transform.position.y + Speed * Time.deltaTime,
	            transform.position.z);

	        if (newVec.y > Upper)
	        {
	            newVec = new Vector3(newVec.x, Upper, newVec.z);
	            Moving = false;
	            LiftSound.Stop();

            }
            
	        transform.position = newVec;

        }
	    else
	    {
	        var newVec = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime,
	            transform.position.z);

	        if (newVec.y < Lower)
	        {
	            Moving = false;
	            newVec = new Vector3(newVec.x, Lower, newVec.z);
	            LiftSound.Stop();
            }

	        transform.position = newVec;
        }
	}
}
