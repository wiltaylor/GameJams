using UnityEngine;
using System.Collections;

public class MeleeAttackController : MonoBehaviour
{
    public float LifeTime = 2f;
    public int Damage = 20;

    public TargetTypes TargetType = TargetTypes.Unit;

    private bool _damageEnabled = true;

    public enum TargetTypes
    {
        Hero,
        Unit
    }

    public void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!_damageEnabled)
            return;

        _damageEnabled = false;
        col.gameObject.SendMessage("TakeHit", Damage);
    }
}
