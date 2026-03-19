using Assets.Systems.CommandManager;
using Assets.Systems.dx;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public float ScrollSpeed;
    public float ZoomSpeed = 5f;
    public float MaxZoom = 10f;
    public float MinZoom = 1f;
    public float DragSpeed = 0.5f;

    private Camera _camera;
    private bool _beginscrolling;


    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

	private void Update ()
	{
	    if (_beginscrolling)
	    {
	        var mouseX = Input.GetAxis("Mouse X");
	        var mouseY = Input.GetAxis("Mouse Y");

            transform.position = new Vector3(transform.position.x - mouseX * DragSpeed, transform.position.y - mouseY * DragSpeed, transform.position.z);
        }

	    if (Input.GetMouseButtonDown(2))
	        _beginscrolling = true;

	    if (Input.GetMouseButtonUp(2))
	        _beginscrolling = false;

        if (Input.GetAxis("Vertical") > 0.1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * ScrollSpeed, transform.position.z);
        }

        if (Input.GetAxis("Vertical") < -0.1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * ScrollSpeed, transform.position.z);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * ScrollSpeed, transform.position.y, transform.position.z);
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * ScrollSpeed, transform.position.y, transform.position.z);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0.1f)
        {
            _camera.orthographicSize -= Time.deltaTime * ZoomSpeed * Input.mouseScrollDelta.y;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < -0.1f)
        {
            _camera.orthographicSize += Time.deltaTime * ZoomSpeed * -Input.mouseScrollDelta.y;
        }

        if(Input.GetButtonUp("NextDialogue"))
            if(DialogueService.Instance.Active)
                DialogueService.Instance.Next();

        if (_camera.orthographicSize < MinZoom)
            _camera.orthographicSize = MinZoom;

        if (_camera.orthographicSize > MaxZoom)
            _camera.orthographicSize = MaxZoom;

        if(Input.GetButtonUp("EndTurn"))
            TurnService.Instance.NextTurn();

        if (Input.GetMouseButtonUp(0))
            CheckClick(0);
        if (Input.GetMouseButtonUp(1))
            CheckClick(1);
    }

    private void CheckClick(int btn)
    {
        var ray = _camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        var hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider == null) return;

        var tile = hit.collider.GetComponent<TileView>();
        CommandService.Instance.ReportTileClick(tile.X, tile.Y, btn);
    }
}

