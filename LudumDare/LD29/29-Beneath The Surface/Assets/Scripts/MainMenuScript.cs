using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{

    public Texture2D HowToPlayImage;
    public Texture2D Logo;
    public Texture2D Backdrop;

    public void OnGUI()
    {

        GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), Backdrop);
        GUI.DrawTexture(new Rect(100, 50, 256, 64), Logo);
        GUI.DrawTexture(new Rect(500, 0, 256, 512), HowToPlayImage);

        if(GUI.Button(new Rect(100, 150, 100, 40), "New Game"))
        {
            Debug.Log("New Game");
            Application.LoadLevel("MainGame");
        }


        if (Application.platform == RuntimePlatform.OSXWebPlayer ||
            Application.platform == RuntimePlatform.WindowsWebPlayer) return;

        if (GUI.Button(new Rect(100, 200, 100, 40), "Exit"))
        {
            Application.Quit();
        }
    }
}
