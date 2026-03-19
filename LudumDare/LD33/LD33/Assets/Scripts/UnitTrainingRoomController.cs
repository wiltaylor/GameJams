using UnityEngine;
using System.Collections;

public class UnitTrainingRoomController : MonoBehaviour
{
    public GameObject SpawnPoint;
    public int DesireOnCreate = 0;
    public int EvilOnCreate = 10;

    public void Start()
    {
        GameManager.Instance.Desire += DesireOnCreate;
        GameManager.Instance.Evil += EvilOnCreate;
    }
}
