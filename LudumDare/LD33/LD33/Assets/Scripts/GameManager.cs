using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int Gold = 0;
    public int Gems = 0;
    public int Evil = 0;
    public int Desire = 0;

    public int GoldPerDrop = 10;
    public int GemsPerDrop = 5;

    public GameObject GoldMinePrefab;
    public GameObject GemMinePrefab;
    public GameObject GameUIPrefab;
    public GameObject GemMineBuilder;
    public BuildQueueManager QueueManager;
    public RoomSettingsManager SettingManager;
    public ShopManager ShopMan;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        ShopMan = GetComponent<ShopManager>();
    }

    void Start()
    {
        RoomManager.Instance.NewLevel();
        var obj = Instantiate(GameUIPrefab);
        QueueManager = obj.GetComponent<BuildQueueManager>();
        SettingManager = obj.GetComponent<RoomSettingsManager>();
    }

    public void DropGold()
    {
        Gold += GoldPerDrop;
    }

    public void DropGems()
    {
        Gems += GemsPerDrop;
    }
}
