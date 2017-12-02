using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<QuestItem>  QuestItems = new List<QuestItem>();

    public float UseDistance = 10f;
    
    public void PickupQuestItem(QuestItem item)
    {
        if(!QuestItems.Contains(item))
            QuestItems.Add(item);
    }

    void Update()
    {
        if (Input.GetButton("Use"))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, UseDistance))
            {
                if (hit.transform.CompareTag("Usable"))
                {
                    hit.transform.gameObject.SendMessage("Use", QuestItems);
                }

            }
            else
            {
                //can't use so grunt or something.
            }
        }
    }

}
