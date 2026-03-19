using UnityEngine;
using System.Collections;

public class SpikeballController : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.gameObject.tag.Contains("Unit")) return;

        var unit = coll.gameObject.GetComponent<UnitController>();
        unit.Kill();
    }
}
