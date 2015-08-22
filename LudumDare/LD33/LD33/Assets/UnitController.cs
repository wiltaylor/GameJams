using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

    public int HP = 100;
    public int MaxHP = 100;

    public void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
