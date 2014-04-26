using UnityEngine;
using System.Collections;

public class Drillable : MonoBehaviour {

    public float Health = 10f;
    public bool Invulnerable = false;

    public void OnBomb()
    {
        if (!Invulnerable)
            Destroy(gameObject);
    }

    public void OnDrill()
    {
        if (Invulnerable)
            return;

        Health -= 1f;

        if (Health < 0f)
        {
            Destroy(gameObject);
        }
    }
}
