using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    public int X;
    public int Y;
    public GUID Id;

    public void Destruct()
    {
        Destroy(gameObject);
    }

}
