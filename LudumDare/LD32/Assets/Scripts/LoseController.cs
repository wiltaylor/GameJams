using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseController : MonoBehaviour {

    public Text Text;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }

    void Update()
    {
        Text.text = _gameController.EnamyUnits.ToString();
    }

    public void PlayAgain()
    {
        _gameController.ReloadLevel();
    }
}
