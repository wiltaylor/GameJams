using UnityEngine;
using System.Collections;

public class PowerUpCollector : MonoBehaviour
{

    public GameObject CurrentItem = null;


    public void GetItem(GameObject item)
    {
        if (CurrentItem == null)
            CurrentItem = item;
        else
            Destroy(item);
    }

    public void UseItem()
    {
        if (CurrentItem == null)
            return;

        CurrentItem.SendMessage("UseItem", gameObject);
        CurrentItem = null;
    }

}
