using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    public GameObject SpawnPrefab;
    public GameObject RoomObject;

    public void Start()
    {
        var obj = (GameObject)Instantiate(SpawnPrefab, transform.position, transform.rotation);
        obj.SendMessage("OnSpawn", RoomObject);
        obj.transform.parent = transform.parent;
        Destroy(gameObject);
    }
}
