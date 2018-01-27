using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NavPoint : MonoBehaviour
{

    public NavPoint NextPoint;
    public float Wait;
    public UnityEvent OnEnterPoint;
}
