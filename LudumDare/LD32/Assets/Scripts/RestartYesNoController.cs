using UnityEngine;
using System.Collections;

public class RestartYesNoController : MonoBehaviour {

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }

    public void Yes()
    {
        _gameController.ReloadLevel();
    }

    public void No()
    {
        gameObject.SetActive(false);
    }

    public void MouseEnterUI()
    {
        _gameController.MouseOverUI = true;
    }

    public void MouseExitUI()
    {
        _gameController.MouseOverUI = false;
    }

}
