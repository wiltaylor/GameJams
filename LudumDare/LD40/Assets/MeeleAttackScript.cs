using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MeeleAttackScript : MonoBehaviour
{
    public float SleepTime = 1f;
    public float Damage = 1f;

    void Update ()
    {
        if (SleepTime < 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            SleepTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var hitbox = other.GetComponent<HitBox>();

        if(hitbox != null)
            hitbox.DoDamage(Damage);
    }
}
