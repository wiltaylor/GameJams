using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{

    public float HP;
    public float DeathTimeOut;
    public UnityEvent OnDeath;
    public UnityEvent OnHit;
    public GameObject LastHitBy;

    private bool _deathTriggered = false;

    public void DoHit(float damage, GameObject attacker)
    {
        LastHitBy = attacker;
        OnHit.Invoke();
    }


	void Update ()
	{
	    if (_deathTriggered)
	        return;

        if (HP <= 0.0f)
        {
            OnDeath.Invoke();
            Destroy(gameObject, DeathTimeOut);
            _deathTriggered = true;
        }
	}
}
