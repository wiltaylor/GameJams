using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public float ScrollSpeed;

    private void Start()
    {
        
    }

	private void Update ()
    {
        if (Input.GetAxis("Vertical") > 0.1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * ScrollSpeed, transform.position.z);
            Debug.Log("Pressed Up");
        }

        if (Input.GetAxis("Vertical") < -0.1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * ScrollSpeed, transform.position.z);
            Debug.Log("Pressed Down");
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * ScrollSpeed, transform.position.y, transform.position.z);
            
            Debug.Log("Pressed Right");
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * ScrollSpeed, transform.position.y, transform.position.z);
            Debug.Log("Pressed Left");
        }



    }
}
