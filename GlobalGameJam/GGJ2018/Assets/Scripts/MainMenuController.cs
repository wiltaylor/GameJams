using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void ApplicationExit()
    {
        Application.Quit();
    }

    public void PlayCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Respawn()
    {
        var killable = FindObjectsOfType<Killable>().FirstOrDefault(o => o.tag == "Player");

        if(killable != null)
            killable.Kill();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
