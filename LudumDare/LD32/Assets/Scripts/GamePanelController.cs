using UnityEngine;
using System.Collections;

public class GamePanelController : MonoBehaviour
{

    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject CommandButtonPanel;
    public float LevelStartGracePeriod= 10f;

    private GameController _gameController;

    void Start()
    {
        _gameController = GameController.Instance;
    }

    void Update()
    {
        if (LevelStartGracePeriod > 0f)
        {
            LevelStartGracePeriod -= Time.deltaTime;
            return;
        }

        if (_gameController.EnamyUnits <= 0)
        {
            WinPanel.SetActive(true);
            LockOutUI();
        }

        if (_gameController.PlayerUnits <= 0)
        {
            LosePanel.SetActive(true);
            LockOutUI();
        }
    }

    void LockOutUI()
    {
        _gameController.UILockedOut = true;
        _gameController.CurrentClickHandler = null;
        CommandButtonPanel.SetActive(false);
        enabled = false;
    }
}
