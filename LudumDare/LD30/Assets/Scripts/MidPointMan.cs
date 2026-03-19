using UnityEngine;
using System.Collections;

public class MidPointMan : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Car")
            return;

        var rankInfo = other.GetComponent<CarRankTracker>();

        rankInfo.HasPassedMidPoint = true;

    }
}
