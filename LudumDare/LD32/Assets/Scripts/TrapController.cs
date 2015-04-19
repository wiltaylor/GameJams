using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Contains("Unit"))
        {
            var unit = coll.gameObject.GetComponent<UnitController>();
            unit.Kill();
        }
    } 
    
}
