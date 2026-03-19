using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {

    public GameObject Prefab;
    public int Quantity = 10;
    public float Time = 10f;

    void Start()
    {
        Invoke("Spawn", Time);
    }

    void Spawn()
    {
        if (Quantity <= 0)
        {
            Destroy(gameObject);
            return;
        }
            

        Instantiate(Prefab, transform.position, transform.rotation);
        Quantity--;

        Invoke("Spawn", Time);
        
    }

   
}
