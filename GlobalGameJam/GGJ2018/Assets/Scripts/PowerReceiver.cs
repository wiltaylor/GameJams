using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerReceiver : MonoBehaviour
{
    public Material PoweredMaterial;
    public Material UnPoweredMaterial;
    public GameObject[] ConnectedObjects;

    private MeshRenderer _render;

    void Start()
    {
        _render = GetComponent<MeshRenderer>();
    }

    public void OnPowerChanged(bool powered)
    {
        if (powered)
            _render.material = PoweredMaterial;
        else
            _render.material = UnPoweredMaterial;


        if (ConnectedObjects != null)
        {
            foreach(var obj in ConnectedObjects)
                obj.SendMessage("OnPowerChanged", powered);
        }
    }

}
