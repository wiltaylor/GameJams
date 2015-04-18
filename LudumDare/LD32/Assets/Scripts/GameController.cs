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
}
