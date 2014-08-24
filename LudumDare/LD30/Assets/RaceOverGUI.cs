using UnityEngine;
using System.Collections;

public class RaceOverGUI : MonoBehaviour
{

    public Texture2D BackgroundImage;

    private GUIStyle _textStyle = new GUIStyle();
    private GUIStyle _buttonStyle = new GUIStyle();

    void Start()
    {
        _textStyle.normal.textColor = Color.red;
        _textStyle.fontSize = 64;
        _textStyle.alignment = TextAnchor.MiddleCenter;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundImage);
        GUI.Label(new Rect(0, 250, Screen.width, 100), "RACE POSITION: " + GameMan.Instance.LastPlayerRank, _textStyle);

        if (GUI.Button(new Rect(20, 400, 100, 50), "RACE AGAIN"))
        {
            Application.LoadLevel(GameMan.Instance.LastRace);
        }

        if (GUI.Button(new Rect(140, 400, 100, 50), "NEW TRACK"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
