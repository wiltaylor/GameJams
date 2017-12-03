using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWallController : MonoBehaviour
{

    public float ClosedHight;
    public float OpenHeight;
    public float OpenSpeed = 10f;
    public bool Opening;

    public void Activate()
    {
        Opening = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Opening)
        {
            var newvec = new Vector3(transform.position.x, transform.position.y - OpenSpeed * Time.deltaTime, transform.position.z);

            if (newvec.y < OpenHeight)
            {
                newvec = new Vector3(transform.position.x, OpenHeight, transform.position.z);
                Opening = false;
            }

            transform.position = newvec;
        }
	}
}
