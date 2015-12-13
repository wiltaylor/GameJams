using UnityEngine;
using System.Collections;

public class LiftController : MonoBehaviour
{
    public string NextLevel;
    public GameObject Trigger;
    public GameObject Door;
    public float CountDown = 5f;
    public bool TriggeredNextLevel;
    public bool ShowMessage = true;

    void Update()
    {
        if (Trigger == null && Door != null)
            Door.SetActive(false);

        if(CountDown <= 0f)
        {
            GlobalController.Instance.LoadLevel(NextLevel);
        }
        else
        {
            if(TriggeredNextLevel)
                CountDown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(ShowMessage)
                GlobalController.Instance.AddMessage("Elevator reached...");
            TriggeredNextLevel = true;
            
        }
    }

}
