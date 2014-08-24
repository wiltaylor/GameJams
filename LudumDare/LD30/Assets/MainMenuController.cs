using System;
using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{

    public Texture2D Logo;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 512, 128), Logo);
    }
}
