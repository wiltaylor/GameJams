using UnityEngine;
using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public IGameClickHandler CurrentClickHandler = null;
    public float YCutOff = -20f;
    public int PlayerUnits = 0;
    public int EnamyUnits = 0;
    public bool MouseOverUI = false;
    public bool UILockedOut = false;

    public AudioMixer Mixer;

    public int BlockersLeft = 5;
    public int DigLeft = 2;
    public int LaddersLeft = 1;
    public int BridgesLeft = 1;

    private LevelInfo _currentLevelInfo;
    private float _initialMusicVol;
    private float _initialSFXVol;

     
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

        Mixer.GetFloat("MusicVol", out _initialMusicVol);
        Mixer.GetFloat("SFXVol", out _initialSFXVol);

        Debug.Log("Music: " + _initialMusicVol.ToString() + " SFX: " + _initialSFXVol.ToString());

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

    public void MuteMusic()
    {
        Mixer.SetFloat("MusicVol", -80f);

    }

    public void MuteSFX()
    {
        Mixer.SetFloat("SFXVol", -80f);
    }

    public void UnMuteMusic()
    {
        Mixer.SetFloat("MusicVol", _initialMusicVol);

    }

    public void UnMuteSFX()
    {
        Mixer.SetFloat("SFXVol", _initialSFXVol);
    }

    public bool IsMusicMuted()
    {
        float val;
        Mixer.GetFloat("MusicVol", out val);

        return val <= -80f;
    }

    public bool IsSFXMuted()
    {
        float val;
        Mixer.GetFloat("SFXVol", out val);

        return val <= -80f;
    }

    public void SkipTutorial()
    {
        PlayerUnits = 0;
        EnamyUnits = 0;
        Application.LoadLevel("Level1");
    }
}


