using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public float MaxX = 1.53f;
    public float MaxY = 1.1f;
    public float MinY = 0.14f;
    public float MinX = 1.35f;
    public float MoveSpeed = 0.01f;
    public float ZoomSpeed = 1f;
    public float MaxZoom = 4f;
    public float MinZoom = 0.5f;

    private Camera _camera;
    private GameController _gameController;

    
	void Start ()
	{
	    _camera = GetComponent<Camera>();
	    _gameController = GameController.Instance;
	}
	
	void Update () {

	    if (Input.GetAxis("Horizontal") > 0f)
	    {
            transform.position = new Vector3(transform.position.x + MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
	    }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            transform.position = new Vector3(transform.position.x - MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        if (Input.GetAxis("Vertical") > 0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + MoveSpeed * Time.deltaTime, transform.position.z);
        }

        if (Input.GetAxis("Vertical") < 0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - MoveSpeed * Time.deltaTime, transform.position.z);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            _camera.orthographicSize += ZoomSpeed*Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            _camera.orthographicSize -= ZoomSpeed * Time.deltaTime;
        }

	    if (_camera.orthographicSize < MinZoom)
	        _camera.orthographicSize = MinZoom;

	    if (_camera.orthographicSize > MaxZoom)
	        _camera.orthographicSize = MaxZoom;

        if (transform.position.x < MinX)
            transform.position = new Vector3(MinX, transform.position.y, transform.position.z);

        if (transform.position.y < MinY)
            transform.position = new Vector3(transform.position.x, MinY, transform.position.z);

        if (transform.position.x > MaxX)
            transform.position = new Vector3(MaxX, transform.position.y, transform.position.z);

        if (transform.position.y > MaxY)
            transform.position = new Vector3(transform.position.x, MaxY, transform.position.z);

	    if (Input.GetMouseButtonUp(0))
	    {
            if (_gameController.CurrentClickHandler != null && _gameController.CurrentClickHandler.SupportWorld && !_gameController.MouseOverUI)
	        {
	            var point = _camera.ScreenToWorldPoint(Input.mousePosition);

	            var ray = _camera.ScreenPointToRay(Input.mousePosition);

	            var hit = Physics2D.GetRayIntersection(ray);

                if(hit.transform == null)
                    _gameController.CurrentClickHandler.ClickWorld(new Vector3(point.x,point.y, 0));
	        }
	    }
	}
}
