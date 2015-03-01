using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public SelectorController Selector;
    public EmpireController Empire;
    public PlanetViewController ViewController;
    public GameObject TransferController;
    
    private LevelGenerator LevelGen;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start ()
	{
	    LevelGen = GetComponent<LevelGenerator>();
        LevelGen.GenerateLevel();
	}
	
}
