using UnityEngine;
using System.Collections;

public class InGameMenuController : MonoBehaviour {

    public GameObject MenuObject;

    public MouseLook _mouseLook;
	
	void Update ()
    {
        if (GlobalController.Instance.CurrentPlayer == null)
            return;

        if (_mouseLook == null)
            _mouseLook = GlobalController.Instance.CurrentPlayer.GetComponent<MouseLook>();

        if (Input.GetButtonDown("Menu"))
        {
            if (MenuObject.activeInHierarchy)
            {
                OnResumeGame();

            }
            else
            {
                MenuObject.SetActive(true);
                GlobalController.Instance.GamePaused = true;
                _mouseLook.MouseLocked = false;
            }
        }
	}

    public void OnRestartLevel()
    {
        GlobalController.Instance.RestartLevel();
    }

    public void OnReturnToMenu()
    {
        GlobalController.Instance.LoadLevel("MainMenu");
    }

    public void OnResumeGame()
    {
        MenuObject.SetActive(false);
        GlobalController.Instance.GamePaused = false;

        if (!GlobalController.Instance.CurrentPlayer.PlayerIsDead)
            _mouseLook.MouseLocked = true;
    }
}
