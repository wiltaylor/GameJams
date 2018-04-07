using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{

    public Text Speed;
    public Image TargetReticle;
    public PlayerController Player;
    public GameObject OnScreenIndicatorPrefab;
    public GameObject OffScreenIndicatorPrefab;

    private Camera _camera = null;

    private readonly List<TrackableObject> _trackableObjects = new List<TrackableObject>();

    void Start()
    {
        _camera = Player.GetComponentInChildren<Camera>();
    }

    public void AddTrackerObject(GameObject obj, Color colour)
    {
        var onscreenTracker = Instantiate(OnScreenIndicatorPrefab);
        onscreenTracker.transform.SetParent(transform);
        onscreenTracker.GetComponent<Image>().color = colour;

        var offscreenTracker = Instantiate(OffScreenIndicatorPrefab);
        offscreenTracker.transform.SetParent(transform);
        offscreenTracker.GetComponent<Image>().color = colour;

        var newTrackObj = new TrackableObject
        {
            Object = obj,
            OnScreenTracker = onscreenTracker,
            OffScreenTracker = offscreenTracker
        };

        _trackableObjects.Add(newTrackObj);
    }

    public void RemoveTrackerObject(GameObject obj)
    {
        _trackableObjects.RemoveAll(o => o.Object == obj);
    }
	
	void Update ()
	{
	    Speed.text = "Speed: " + Player.CurrentSpeed;
        TargetReticle.enabled = Player.FreeLookOn;

        if(_camera == null)
            return;

	    _trackableObjects.RemoveAll(o => o.Object == null);

	    foreach (var obj in _trackableObjects)
	    {
	        var screenPoint = _camera.WorldToScreenPoint(obj.Object.transform.position);

	        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.y > 0 && screenPoint.x < Screen.width && screenPoint.y < Screen.height)
	        {
                obj.OffScreenTracker.SetActive(false);
                obj.OnScreenTracker.SetActive(true);
	            var rect = obj.OnScreenTracker.GetComponent<RectTransform>();
	            rect.transform.position = screenPoint;
	        }
	        else //Offscreen
	        {
	            obj.OffScreenTracker.SetActive(true);
                obj.OnScreenTracker.SetActive(false);

                //To see how this code works see https://youtu.be/gAQpR1GN0Os

                if (screenPoint.z < 0)
	                screenPoint *= -1;

                var screenCenter = new Vector3(Screen.width, Screen.height, 0) /2;

	            screenPoint -= screenCenter;

	            var angle = Mathf.Atan2(screenPoint.y, screenPoint.x);
	            angle -= 90 * Mathf.Deg2Rad;

                var cos = Mathf.Cos(angle);
	            var sin = -Mathf.Sin(angle);

	            screenPoint = screenCenter + new Vector3(sin * 150, cos * 150, 0);

	            var m = cos / sin;

	            var screenBounds = screenCenter * 0.95f;

	            screenPoint = cos > 0 ? new Vector3(screenBounds.y / m, screenBounds.y, 0) : new Vector3(-screenBounds.y / m, -screenBounds.y, 0);

                if(screenPoint.x > screenBounds.x)
                    screenPoint = new Vector3(screenBounds.x, screenBounds.x * m, 0);
                else if(screenPoint.x < -screenBounds.x)
                    screenPoint = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);

	            screenPoint += screenCenter;

	            var rect = obj.OffScreenTracker.GetComponent<RectTransform>();
	            rect.transform.position = screenPoint;
	            rect.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
               
            }
        }

	}

}
