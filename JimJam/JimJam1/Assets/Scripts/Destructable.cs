using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;

public enum DestructableFaction
{
    None,
    Pirate,
    Police
}

public class Destructable : MonoBehaviour
{

    public float HP;
    public float MaxHP;
    public float Shield;
    public float MaxShield;
    public float ShieldRechargeRate;
    public UnityEvent OnDeath;
    public UnityEvent OnHit;
    public GameObject LastHitBy;
    public DestructableFaction Faction;

    public void DoHit(float damage, GameObject attacker)
    {
        if (Shield > 0f)
        {
            Shield -= damage;

            if (Shield < 0f)
            {
                damage = -1 * Shield;
                Shield = 0f;
            }
            else
            {
                damage = 0f;
            }
        }

        HP -= damage;

        LastHitBy = attacker;
        OnHit.Invoke();
    }

    public void DestroyMe(float time)
    {
        Destroy(gameObject, time);
    }


	void Update ()
	{
        if (HP <= 0.0f)
        {
            OnDeath.Invoke();
        }

	    if (Shield < MaxShield)
	    {
	        Shield += ShieldRechargeRate * Time.deltaTime;
	    }
	    else
	    {
	        Shield = MaxShield;
	    }
	}
}
