using UnityEngine;
using System.Collections;

public class LavaHandler : MonoBehaviour
{

    public float DamageAmmount = 5f;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (DamageAmmount == 0)
            return;

        if(collider.tag == "Player")
            collider.gameObject.SendMessage("Damage", DamageAmmount);
    }

    void OnDrill(GameObject player)
    {
        //do nothing.
    }
}
