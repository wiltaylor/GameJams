using UnityEngine;
using System.Collections;

public class ResourceRoomController : MonoBehaviour
{
    public GameObject SpawnPoint;
    public GameObject Units;
    public GameObject ResourceDrop;
    public GameObject ResourceEnterance;
    public GameObject BottomOfLadder;
    public GameObject TopOfLadder;
    public ResourceTypes ResourceType;
    public int MaxWorkers = 5;

    public enum ResourceTypes
    {
        Gold,
        Gems
    }

    public void AddUnit(GameObject prefab)
    {
        var unit = Instantiate(prefab);
        unit.transform.parent = Units.transform;
        unit.SendMessage("OnSpawn", gameObject);
    }

    public int GetCurrentWorkers()
    {
        int workers = 0;

        for (int i = 0; i < Units.transform.childCount; i++)
        {
            if (Units.transform.GetChild(i).name.StartsWith("Imp"))
                workers++;
        }

        return workers;

    }
}
