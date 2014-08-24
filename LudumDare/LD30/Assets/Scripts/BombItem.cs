using UnityEngine;
using System.Collections;

public class BombItem : MonoBehaviour {

    public GameObject BombPrefab;

    public void UseItem(GameObject user)
    {
        var bomb = ((GameObject)Instantiate(BombPrefab)).GetComponent<BombController>();
        bomb.transform.position = user.transform.position;
        bomb.transform.rotation = user.transform.rotation;

        Destroy(gameObject);
    }
}
