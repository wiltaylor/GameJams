using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinController : MonoBehaviour {

    public Text Text;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }

    void Update()
    {
        Text.text = _gameController.PlayerUnits.ToString();
    }

    public void PlayAgain()
    {
        _gameController.ReloadLevel();
    }

    public void NextLevel()
    {
        _gameController.NextLevel();
    }
}
