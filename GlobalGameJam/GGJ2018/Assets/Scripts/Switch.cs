using System;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public float ChangetimeOut = 2f;
    public UnityEvent OnActivate;


    private float _currentchangetimeout = 0f;

    void Start()
    {
        
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


        OnActivate.Invoke();

        _currentchangetimeout = ChangetimeOut;
    }
}
