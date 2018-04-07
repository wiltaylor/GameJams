using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerTag : MonoBehaviour
{
    public Color Colour;
    void Start()
    {
        var hud = GameObject.FindWithTag("HUD").GetComponent<PlayerHUDController>();

        hud.AddTrackerObject(gameObject, Colour);
        
    }
}
