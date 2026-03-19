using UnityEngine;
using System.Collections;

public class AITargetController : MonoBehaviour
{

    public AIController AITargeter;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AITargeter.Target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            AITargeter.Target = null;
        }
    }
}
