using System;
using UnityEngine;
using System.Collections;

public class PlayerUIController : MonoBehaviour
{
    private PowerUpCollector _powerUpCollector;
    private GameObject _lastObject;
    private Texture _powerUpIcon;
    private CarRankTracker _rankTracker;
    private GUIStyle _ScoreStyle = new GUIStyle();

    void Start ()
	{
	    _powerUpCollector = GetComponent<PowerUpCollector>();
        _rankTracker = GetComponent<CarRankTracker>();

        _ScoreStyle.normal.textColor = Color.red;
        _ScoreStyle.fontSize = 32;
        _ScoreStyle.alignment = TextAnchor.UpperCenter;
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

        var speed = Math.Floor(rigidbody2D.velocity.magnitude*3600/1000);
        var scoreString = string.Format("Speed: {0} Rank: {1}/10 Lap: {2}/3", speed, _rankTracker.Rank, _rankTracker.Lap);


        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), scoreString, _ScoreStyle);
    }
}
