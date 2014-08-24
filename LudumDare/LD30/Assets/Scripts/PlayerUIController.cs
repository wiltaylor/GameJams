using System;
using UnityEngine;
using System.Collections;

public class PlayerUIController : MonoBehaviour
{
    private PowerUpCollector _powerUpCollector;
    private GameObject _lastObject;
    private Texture _powerUpIcon;
    private CarRankTracker _rankTracker;

    void Start ()
	{
	    _powerUpCollector = GetComponent<PowerUpCollector>();
        _rankTracker = GetComponent<CarRankTracker>();
	}
	
	void Update ()
	{
	    if (_powerUpCollector.CurrentItem == null)
	        _powerUpIcon = null;

	    if (_lastObject == _powerUpCollector.CurrentItem) return;

	    _lastObject = _powerUpCollector.CurrentItem;

	    if (_lastObject == null)
	    {
	        _powerUpIcon = null;
	        return;

	    }

	    var info = _lastObject.GetComponent<PowerUpInfo>();

        _powerUpIcon = info.Icon.texture;
    }

    void OnGUI()
    {
        if (_powerUpIcon != null)
        {
            GUI.DrawTexture(new Rect(0,0, 64,64), _powerUpIcon, ScaleMode.ScaleToFit, true, 1f);
        }

        GUI.Label(new Rect(Screen.width / 2 - 10, 0, 20, 20), Math.Floor(rigidbody2D.velocity.magnitude * 3600 / 1000).ToString());
        GUI.Label(new Rect(Screen.width - 20, 0, 20, 20), _rankTracker.Rank.ToString());
    }
}
