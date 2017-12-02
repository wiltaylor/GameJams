using UnityEngine;


public enum DoorState
{
    Closed,
    Opening,
    Open,
    Closing
}

public class DoorController : MonoBehaviour
{

    public DoorState State = DoorState.Closed;
    public float DoorSpeed = 1f;
    public float DoorBottom;
    public float DoorTop;
    public float DoorOpenTimeOut = 10f;
    
    private float _currentOpenTimeout;
	
	void Update ()
    {
        if (State == DoorState.Open)
        {
            _currentOpenTimeout -= Time.deltaTime;

            if (_currentOpenTimeout < 0)
                State = DoorState.Closing;
        }
        else
        {
            _currentOpenTimeout = DoorOpenTimeOut;
        }

        if (State == DoorState.Opening)
        {
            var newPos = new Vector3(transform.localPosition.x, transform.localPosition.y - DoorSpeed * Time.deltaTime, transform.localPosition.z);

            if (newPos.y < DoorBottom)
            {
                newPos = new Vector3(transform.localPosition.x, DoorBottom, transform.localPosition.z);
                State = DoorState.Open;
            }

            transform.localPosition = newPos;
        }

        if (State == DoorState.Closing)
        {
            var newPos = new Vector3(transform.localPosition.x, transform.localPosition.y + DoorSpeed * Time.deltaTime, transform.localPosition.z);

            if (newPos.y > DoorTop)
            {
                newPos = new Vector3(transform.localPosition.x, DoorTop, transform.localPosition.z);
                State = DoorState.Closed;
            }

            transform.localPosition = newPos;
        }
    }
    
    public void OpenDoor()
    {
        if (State == DoorState.Closed || State == DoorState.Closing)
            State = DoorState.Opening;

    }
}
