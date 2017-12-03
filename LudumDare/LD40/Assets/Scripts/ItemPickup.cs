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
    public WeaponItem Weapon;
    public AudioSource Sound;

    private bool _pickedUp;

    void OnTriggerEnter(Collider other)
    {
        if (_pickedUp)
            return;

        

        if (other.CompareTag("Player"))
        {
            _pickedUp = true;

            if (Item != null)
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

            if(Weapon != null)
                other.gameObject.SendMessage("PickUpWeapon", Weapon);

            if(Sound != null)
                Sound.Play();

            Destroy(gameObject, 1f);
        }
    }
}
