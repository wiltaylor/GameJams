using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health TargetHealth;
    public float Modifer;


    public void DoDamage(float ammount)
    {
        TargetHealth.DoDamage(ammount * Modifer);
    }
}
