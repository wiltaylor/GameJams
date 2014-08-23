using System;
using UnityEngine;
using System.Collections;

public class PlayerUIController : MonoBehaviour
{
    private PowerUpCollector _powerUpCollector;
    private GameObject _lastObject;
    private Texture _powerUpIcon;
    private float _lastSpeed = 0f;

    public PlayerUIController(GameObject lastObject, float lastSpeed)
    {
        _lastObject = lastObject;
        _lastSpeed = lastSpeed;
    }

    void Start ()
	{
	    _powerUpCollector = GetComponent<PowerUpCollector>();
	}

    void FixedUpdate()
    {
        _lastSpeed = rigidbody2D.velocity.magnitude;
    }
	
	void Update () 
    {
	    if (_lastObject == _powerUpCollector.CurrentItem) return;

	    _lastObject = _powerUpCollector.CurrentItem;

	    var info = _lastObject.GetComponent<PowerUpInfo>();

        _powerUpIcon = info.Icon.texture;
    }

    void OnGUI()
    {
        if (_powerUpIcon != null)
        {
            GUI.DrawTexture(new Rect(0,0, 64,64), _powerUpIcon, ScaleMode.ScaleToFit, true, 1f);
        }

        GUI.Label(new Rect(20, 0, 20, 20), Math.Floor(_lastSpeed).ToString());
    }
}
