using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject ExitButton;
    public GameObject HowToPlay;

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
        HowToPlay.SetActive(true);
        HowToPlay.GetComponent<HowToPlayController>().Show();
    }

    public void ClickNewGame()
    {
        Application.LoadLevel("MainGame");
    }
}
