using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int Gold = 0;
    public int Food = 0;
    public int Gems = 0;
    public int Evil = 0;
    public int Desire = 0;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

}
