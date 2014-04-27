using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{

    public Texture2D HowToPlayImage;
    public Texture2D Logo;
    public Texture2D Backdrop;
    public Texture2D StartButton;
    public GUISkin GUINormal;

    public void OnGUI()
    {

        var musicSettingString = "Music: ";
        var sfxSettingString = "SFX: ";

        if (GlobalGameSettings.Instance.MusicEnabled)
            musicSettingString += "ON";
        else
            musicSettingString += "OFF";

        if (GlobalGameSettings.Instance.SoundEnabled)
            sfxSettingString += "ON";
        else
            sfxSettingString += "OFF";

        GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), Backdrop);
        GUI.DrawTexture(new Rect(100, 50, 256, 64), Logo);
        GUI.DrawTexture(new Rect(500, 0, 256, 512), HowToPlayImage);

        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.skin = GUINormal;

        if (GUI.Button(new Rect(100, 150, 200, 40), "Start Game"))
        {
            Debug.Log("New Game");
            Application.LoadLevel("MainGame");
        }

        if (GUI.Button(new Rect(100, 200, 200, 40), musicSettingString))
        {
            GlobalGameSettings.Instance.MusicEnabled = !GlobalGameSettings.Instance.MusicEnabled;
        }

        if (GUI.Button(new Rect(100, 250, 200, 40), sfxSettingString))
        {
            GlobalGameSettings.Instance.SoundEnabled = !GlobalGameSettings.Instance.SoundEnabled;
        }


        if (Application.platform == RuntimePlatform.OSXWebPlayer ||
            Application.platform == RuntimePlatform.WindowsWebPlayer) return;

        if (GUI.Button(new Rect(100, 300, 200, 40), "Exit"))
        {
            Application.Quit();
        }
    }
}
