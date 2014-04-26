using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float HurtTimeOut = 1f;
    public float Health = 20f;
    public float MaxHealth = 20f;
    public int Money = 0;
    public AudioClip MoneySound;
    public AudioClip HurtSound;
    public AudioClip HealthSound;

    private AudioSource _audioSource;
    private float _hurtTime = -1f;
	// Use this for initialization
	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Health < 0f)
	    {
	        gameObject.SetActive(false);
	    }

	    if (_hurtTime > 0f)
	        _hurtTime -= Time.deltaTime;
	}

    void Damage(float ammout)
    {
        Health -= ammout;

        if (_hurtTime < 0f)
        {
            AudioSource.PlayClipAtPoint(HurtSound, transform.position);
            _hurtTime = HurtTimeOut;
        }

    }

    void PickUpCash(int ammount)
    {
        Money += ammount;
        AudioSource.PlayClipAtPoint(MoneySound, transform.position);
    }

    void PickUpHealth(float ammount)
    {
        Health += ammount;
        if (Health > MaxHealth)
            Health = MaxHealth;

        AudioSource.PlayClipAtPoint(HealthSound, transform.position);
    }
}
