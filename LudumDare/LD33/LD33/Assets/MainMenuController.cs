using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject ExitButton;

    public void Start()
    {
        if (Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ExitButton.SetActive(true);
        }
        else
        {
            ExitButton.SetActive(false);
        }
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void ClickHowToPlay()
    {
        //Create tutorial.
    }

    public void ClickNewGame()
    {
        Application.LoadLevel("MainGame");
    }
}
