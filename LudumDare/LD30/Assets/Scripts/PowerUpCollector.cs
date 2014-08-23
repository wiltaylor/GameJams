using UnityEngine;
using System.Collections;

public class PowerUpCollector : MonoBehaviour
{

    public GameObject CurrentItem = null;


    public void GetItem(GameObject item)
    {
        if (CurrentItem == null)
            CurrentItem = item;
    }

    public void UseItem()
    {
        
    }

}
