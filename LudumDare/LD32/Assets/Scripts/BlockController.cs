using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

    public bool Indestructable = false;
    public bool Targeted = false;
    public float Health = 10;

    void FixedUpdate()
    {
        if (Health < 0f)
        {
            Destroy(gameObject);
        }
    }
}
