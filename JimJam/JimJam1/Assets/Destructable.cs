using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{

    public float HP;
    public float DeathTimeOut;
    public UnityEvent OnDeath;

    private bool _deathTriggered = false;

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
