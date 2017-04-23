using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    public int X;
    public int Y;

    public void Destruct()
    {
        Destroy(gameObject);
    }

}
