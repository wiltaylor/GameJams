using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerHandler : MonoBehaviour
{
    public UnityEvent OnTrigger;
    public UnityEvent OnExitTrigger;

    public GameObject Parent;
    public bool ParentOnEnter;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTrigger.Invoke();

            if (ParentOnEnter)
                other.transform.SetParent(Parent.transform);
        }

        

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnExitTrigger.Invoke();

            if (ParentOnEnter)
                other.transform.SetParent(null);

        }
    }

}
