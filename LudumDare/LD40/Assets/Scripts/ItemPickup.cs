using Assets.Scripts;
using UnityEngine;

public class AmmoPickUp
{
    public AmmoType Type;
    public int Ammount;
}

public class ItemPickup : MonoBehaviour
{
    public QuestItem Item;
    public float Health;
    public int Ammo;
    public AmmoType AmmoType;
    public int Gold;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Item != null)
                other.gameObject.SendMessage("PickupQuestItem", Item);

            if(Health > 0f)
                other.gameObject.SendMessage("PickUpHealth", Health);

            if (AmmoType != null)
            {
                other.gameObject.SendMessage("PickUpAmmo", new AmmoPickUp { Type = AmmoType, Ammount =  Ammo});
            }

            if (Gold > 0)
            {
                other.gameObject.SendMessage("PickUpGold", Gold);
            }

            Destroy(gameObject);
        }
    }
}
