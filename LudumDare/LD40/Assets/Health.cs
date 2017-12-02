using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public bool Invincible;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;

    public void DoDamage(float ammount)
    {
        if (Invincible)
            return;

        CurrentHealth -= ammount;

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
    }
}
