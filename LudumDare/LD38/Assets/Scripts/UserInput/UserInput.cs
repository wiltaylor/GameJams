using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public float ScrollSpeed;
    public float ZoomSpeed = 5f;
    public float MaxZoom = 10f;
    public float MinZoom = 1f;

    private Camera _camera;


    private void Start()
    {
        _camera = GetComponent<Camera>();
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

        if (Input.GetAxis("Mouse ScrollWheel") > 0.1f)
        {
            Debug.Log("zoom in");
            _camera.orthographicSize -= Time.deltaTime * ZoomSpeed;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < -0.1f)
        {
            Debug.Log("zoom out");
            _camera.orthographicSize += Time.deltaTime * ZoomSpeed;
        }

        if (_camera.orthographicSize < MinZoom)
            _camera.orthographicSize = MinZoom;

        if (_camera.orthographicSize > MaxZoom)
            _camera.orthographicSize = MaxZoom;
    }
}
