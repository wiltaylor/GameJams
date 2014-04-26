using UnityEngine;
using System.Collections;

public class DrillScript : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        collider.gameObject.SendMessage("OnDrill");
    }
}
