using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public enum RotationDirection
    {
        Left,
        Right,
        Forward,
        Back
    }

    public float Speed;
    public bool Always;

    private Vector3 _targetRotation;

    // Update is called once per frame
	void Update ()
    {
        if (Always)
        {
            transform.Rotate(0, Speed * Time.deltaTime, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.eulerAngles, _targetRotation, Time.deltaTime * Speed));
        }

        
	}

    public void Rotate(float angle)
    {
        _targetRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + angle, transform.rotation.eulerAngles.z);
    }
}
