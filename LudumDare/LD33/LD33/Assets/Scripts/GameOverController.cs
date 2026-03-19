using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public GameObject GameOverScreen;

    public void ShowGameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void ClickPlayAgain()
    {
        Application.LoadLevel("MainGame");
    }

    public void ClickMainMenu()
    {
        Application.LoadLevel("Menu");
    }
}
