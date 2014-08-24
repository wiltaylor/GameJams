using System;
using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{

    public Texture2D Logo;
    public Texture2D Level1Button;
    public Texture2D Level2Button;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 512, 128), Logo);

        if (GUI.Button(new Rect(20, 120, 256, 128), Level1Button))
        {
            Application.LoadLevel("TestLevel");
        }

        if (GUI.Button(new Rect(286, 120, 256, 128), Level2Button))
        {
            Application.LoadLevel("AlienLevel");
        }

    }
}
