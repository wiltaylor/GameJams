using UnityEngine;
using System.Collections;

public class Drillable : MonoBehaviour {

    public float Health = 10f;
    public bool Invulnerable = false;
    public int MoneyOnDestruct = 0;
    public float LifeGainOnDestruct = 0;

    public void OnBomb()
    {
        if (!Invulnerable)
            Destroy(gameObject);
    }

    public void OnDrill(GameObject player)
    {
        if (Invulnerable)
            return;

        Health -= 1f;

        if (Health < 0f)
        {
            if(MoneyOnDestruct != 0)
                player.SendMessage("PickUpCash", MoneyOnDestruct);

            if(LifeGainOnDestruct != 0)
                player.SendMessage("PickUpHealth", LifeGainOnDestruct);

            Destroy(gameObject);
        }
    }
}
