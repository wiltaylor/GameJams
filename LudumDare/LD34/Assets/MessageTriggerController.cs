using UnityEngine;
using System.Collections;

public class MessageTriggerController : MonoBehaviour
{
    public string[] Messages;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(var msg in Messages)
            {
                GlobalController.Instance.AddMessage(msg);
            }

            Destroy(gameObject);
        }
    }
}
