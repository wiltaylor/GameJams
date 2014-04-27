using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{
    public float XAxis { get; private set; }
    public float YAxis { get; private set; }
    public float UIMultiplyer = 0.5f;
    
    private bool _joystickInUse = false;
    private int _fingerID = -1;
    private GUITexture _button;
    private Vector3 _rootPosition;
    

	void Start () 
    {
	    if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.MetroPlayerARM &&
	        Application.platform != RuntimePlatform.MetroPlayerX64 &&
	        Application.platform != RuntimePlatform.MetroPlayerX86)
	    {
	        gameObject.SetActive(false);
	    }

	    _rootPosition = transform.position;
    }
	
	void Update () {
	    foreach (var i in Input.touches)
	    {
	        if (guiTexture.HitTest(i.position))
	        {
	            if (i.phase == TouchPhase.Began)
	            {
	                if (_joystickInUse && _fingerID != i.fingerId)
	                    continue;

	                _joystickInUse = true;
	                _fingerID = i.fingerId;
	            }

	            if (i.phase == TouchPhase.Moved)
	            {
	                XAxis = i.deltaPosition.normalized.x;
	                YAxis = i.deltaPosition.normalized.y;

                    transform.position = _rootPosition + new Vector3(i.deltaPosition.normalized.x, i.deltaPosition.normalized.y) * UIMultiplyer;
	            }

	            if (i.phase == TouchPhase.Ended || i.phase == TouchPhase.Canceled)
	            {
	                if (i.fingerId == _fingerID)
	                    _fingerID = -1;
	                    _joystickInUse = false;

	                transform.position = _rootPosition;
	            }
	        }
	    }

        

	}
}
