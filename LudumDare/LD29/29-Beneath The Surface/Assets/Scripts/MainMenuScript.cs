using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{

    private bool howToPlayer = false;

    public void OnGUI()
    {
        if (howToPlayer)
        {
            GUI.BeginGroup(new Rect(500, 0, 400, 400));
            GUI.Box(new Rect(0,0, 400, 400), "How to play");
            GUI.EndGroup();
        }

        if(GUI.Button(new Rect(100, 100, 100, 40), "New Game"))
        {
            Debug.Log("New Game");
            Application.LoadLevel("MainGame");
        }

        if (GUI.Button(new Rect(100, 150, 100, 40), "Settings"))
        {
            howToPlayer = !howToPlayer;
        }

        if (Application.platform == RuntimePlatform.OSXWebPlayer ||
            Application.platform == RuntimePlatform.WindowsWebPlayer) return;

        if (GUI.Button(new Rect(100, 200, 100, 40), "Exit"))
        {
            Application.Quit();
        }
    }
}
