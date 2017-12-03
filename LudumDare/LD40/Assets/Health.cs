using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public bool Invincible;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;
    public PlayerStats PlayerStats;

    void Start()
    {
        if (PlayerStats != null)
        { 
            PlayerStats.CurrentHP = CurrentHealth;
            PlayerStats.MaxHP = MaxHealth;
        }
    }

    public void DoDamage(float ammount)
    {
        if (Invincible)
            return;

        CurrentHealth -= ammount;

        if(PlayerStats != null)
        PlayerStats.CurrentHP = CurrentHealth;

        if(CurrentHealth > 0f)
            OnHit.Invoke();
        else
            OnDeath.Invoke();
    }

    public void PickUpHealth(float ammount)
    {
        CurrentHealth += ammount;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        if (PlayerStats != null)
            PlayerStats.CurrentHP = CurrentHealth;
    }
}
