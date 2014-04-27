using UnityEngine;
using System.Collections;

public class MonsterHit : MonoBehaviour
{

    public float DamageOnHit = 10f;
    public MonsterAI AIController;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (DamageOnHit == 0f)
            return;

        if (collider.tag != "Player")
            return;

        var health = collider.GetComponent<PlayerHealth>();

        if (health.CurrentHurtTime < 0f)
            collider.GetComponent<PlayerHealth>().Damage(DamageOnHit);

        AIController.DoTimeOut();
    }

    void OnDrill(GameObject player)
    {
        //do nothing.
    }
}
