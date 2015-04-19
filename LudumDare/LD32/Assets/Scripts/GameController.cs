using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public IGameClickHandler CurrentClickHandler = null;
    public float YCutOff = -20f;
    public int PlayerUnits = 0;
    public int EnamyUnits = 0;
    public bool MouseOverUI = false;
    public bool UILockedOut = false;

    public int BlockersLeft = 5;
    public int DigLeft = 2;
    public int LaddersLeft = 1;
    public int BridgesLeft = 1;

    private LevelInfo _currentLevelInfo;
     
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);

        CurrentClickHandler = new CreateBlockerCommand();
    }

    void Start()
    {
        OnLevelWasLoaded(Application.loadedLevel);
    }

    void OnLevelWasLoaded(int level)
    {
        _currentLevelInfo = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();

        if (_currentLevelInfo == null)
        {
           Debug.LogError("No Level Info Object found!");
           return;
        }

        YCutOff = _currentLevelInfo.YCutOff;
        BlockersLeft = _currentLevelInfo.Blockers;
        DigLeft = _currentLevelInfo.DigLeft;
        LaddersLeft = _currentLevelInfo.Ladders;
        BridgesLeft = _currentLevelInfo.Bridges;

        UILockedOut = false;
        CurrentClickHandler = null;
    }

    public void NextLevel()
    {
        PlayerUnits = 0;
        EnamyUnits = 0;
        Application.LoadLevel(_currentLevelInfo.NextLevel);
    }

    public void ReloadLevel()
    {
        PlayerUnits = 0;
        EnamyUnits = 0;
        Application.LoadLevel(Application.loadedLevelName);
    }
}
