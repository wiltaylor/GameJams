using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public List<PlanetController> PlanetDirectory;
    public static GameManager Instance;
    public SelectorController Selector;
    public EmpireController Empire;
    public PlanetViewController ViewController;
    public GameObject TransferController;
    public GameObject SpaceBattleController;
    public GameObject GroundBattleController;
    public GameObject WaitController;
    public GameObject YouWinController;

    public int Year;
    
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

    public void PassTime(int years)
    {
        foreach (var p in PlanetDirectory.Where(p => p.PlayerOwned))
        {
            p.Troops += years*p.TroopProductionRate;
            p.Ships += years * p.ShipProductionRate;
            p.Food += years * p.FoodProductionRate;
            p.Fuel += years * p.FuelProductionRate;
        }

        Year += years;

        CheckForWin();
    }

    public void CheckForWin()
    {
        YouWinController.SetActive(true);
        YouWinController.GetComponent<WinController>().StartWinController();
    }
}
