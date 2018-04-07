using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackerType
{
    Gate,
    Items,
    Station,
    Enemy,
    Friendly
}

public class TrackerTag : MonoBehaviour
{
    public Color Colour;
    public string Text;
    public TrackerType Type;
    void Start()
    {
        var hud = GameObject.FindWithTag("HUD").GetComponent<PlayerHUDController>();

        hud.AddTrackerObject(gameObject, Colour, Text);
        
    }
}
