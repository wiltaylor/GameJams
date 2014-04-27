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
    public float CurrentHurtTime = -1f;
	// Use this for initialization
	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Health <= 0f)
	    {
	        gameObject.SetActive(false);
	    }

	    if (CurrentHurtTime > 0f)
	        CurrentHurtTime -= Time.deltaTime;
	}

    void Damage(float ammout)
    {
        Health -= ammout;

        if (CurrentHurtTime < 0f)
        {
            CurrentHurtTime = HurtTimeOut;
            if (GlobalGameSettings.Instance.SoundEnabled)
                AudioSource.PlayClipAtPoint(HurtSound, transform.position);
        }
    }

    void PickUpCash(int ammount)
    {
        Money += ammount;
        if (GlobalGameSettings.Instance.SoundEnabled)
            AudioSource.PlayClipAtPoint(MoneySound, transform.position);
    }

    void PickUpHealth(float ammount)
    {
        Health += ammount;
        if (Health > MaxHealth)
            Health = MaxHealth;

        if (GlobalGameSettings.Instance.SoundEnabled)
            AudioSource.PlayClipAtPoint(HealthSound, transform.position);
    }
}
