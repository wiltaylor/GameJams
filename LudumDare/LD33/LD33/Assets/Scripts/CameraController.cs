using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public float MoveSpeed = 1f;

    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

	void Update ()
    {

	    if (Input.GetKey(KeyCode.LeftArrow))
	    {
	        transform.position = new Vector3(transform.position.x - MoveSpeed, transform.position.y, transform.position.z);
	    }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + MoveSpeed, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + MoveSpeed, transform.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - MoveSpeed, transform.position.z);
        }



    }
}
