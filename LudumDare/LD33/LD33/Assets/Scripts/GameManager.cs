using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int Gold = 0;
    public int Food = 0;
    public int Gems = 0;
    public int Evil = 0;
    public int Desire = 0;

    public int GoldPerDrop = 10;

    public GameObject GoldMinePrefab;
    public GameObject GameUIPrefab;

    public static GameManager Instance;

    public enum RoomType
    {
        GoldMine
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        RoomManager.Instance.NewLevel();
        Instantiate(GameUIPrefab);
    }

    public void DropGold()
    {
        Gold += GoldPerDrop;
    }

    public void BuyRoom(RoomType room)
    {
        switch (room)
        {
            case RoomType.GoldMine:
                RoomManager.Instance.AddRoom(Instantiate(GoldMinePrefab));
                break;
        }
    }

}
