using System;
using UnityEngine;

public class BeamInput : MonoBehaviour
{

    public GameObject[] AttachedObjects;
    public bool Powered;
    public float ChangetimeOut = 2f;


    private float _currentchangetimeout = 0f;

    void Start()
    {
        foreach (var obj in AttachedObjects)
            obj.SendMessage("OnPowerChanged", Powered);
    }

    void Update()
    {
        if (_currentchangetimeout > 0f)
            _currentchangetimeout -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("OnContext", gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("ClearContext", gameObject);
        }
    }

    public void OnUse()
    {

        if (_currentchangetimeout > 0f)
            return;

        Powered = !Powered;

        foreach(var obj in AttachedObjects)
            obj.SendMessage("OnPowerChanged", Powered);

        _currentchangetimeout = ChangetimeOut;
    }
}
