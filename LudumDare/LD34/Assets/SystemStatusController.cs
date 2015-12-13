using UnityEngine;
using System.Collections;

public class SystemStatusController : MonoBehaviour {

    public LedController Engine;
    public LedController Breaks;
    public LedController Steering;
    public LedController Weapons;

    // Update is called once per frame
    void Update ()
    {
        Engine.Ok = true;
        Breaks.Ok = GlobalController.Instance.HasBreak;
        Steering.Ok = GlobalController.Instance.HasSteering;
        Weapons.Ok = GlobalController.Instance.HasGuns;
	}
}
