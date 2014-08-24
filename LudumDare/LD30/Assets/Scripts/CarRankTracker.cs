using UnityEngine;
using System.Collections;

public class CarRankTracker : MonoBehaviour
{

    public int Lap = 0;
    public int Rank = 0;
    public float NodeDistance = 0f;
    public int NodeID = 0;
    public GameObject NextNode;
    public bool IsPlayer = false;
    public bool HasPassedMidPoint = false;
}
