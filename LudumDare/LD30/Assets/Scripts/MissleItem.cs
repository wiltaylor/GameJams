using UnityEngine;
using System.Collections;

public class MissleItem : MonoBehaviour
{

    public GameObject MissilePrefab;

    public void UseItem(GameObject user)
    {
        var missile = ((GameObject)Instantiate(MissilePrefab)).GetComponent<MissileController>();
        missile.Spawner = user;
        missile.transform.position = user.transform.position;
        missile.transform.rotation = user.transform.rotation;

        Destroy(gameObject);
    }
}
