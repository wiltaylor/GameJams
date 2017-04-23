using System.Collections;
using System.Collections.Generic;
using Assets.Systems.Unit;
using UnityEngine;

public class WTDebug : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		Invoke("Debug", 3f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Debug()
    {
        UnitService.Instance.AddUnit(20, 20, UnitType.Scout, UnitFaction.Player);
    }
}
