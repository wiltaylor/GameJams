using Assets.Scripts;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public QuestItem Item;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Item != null)
                other.gameObject.SendMessage("PickupQuestItem", Item);

            Destroy(gameObject);
        }
    }
}
