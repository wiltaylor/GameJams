using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIState
{
    Idle,
    Attacking,
    MovingToAttack,
    Moving,
    Sleep
}

public class ShipAI : MonoBehaviour
{
    public AIState State;
    public bool HostileToPlayer;
    public GameObject Target;
    public Vector3 MoveTarget;
    public float AttackAtDistance;
    public float OutOfRangeDistance;
    public float FireDuration;
    public float FireCoolDown;
    public float Speed;
    public float MoveToDistance = 0.1f;

    public AIWeaponController[] AIWeapons;

    private float _fireDuration;
    private float _fireCoolDown = 0f;
    private Destructable _destructable;


    void Update()
    {
        switch (State)
        {
            case AIState.Idle:
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
            case AIState.MovingToAttack:
                MovingToAttackUpdate();
                break;
            case AIState.Moving:
                MovingUpdate();
                break;
            case AIState.Sleep:
                SleepUpdate();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void Start()
    {
        _destructable = GetComponent<Destructable>();
    }

    public void OnShot()
    {
        if (_destructable.LastHitBy == null)
            return;

        Target = _destructable.LastHitBy;
        State = AIState.MovingToAttack;
    }

    void AttackingUpdate()
    {
        if (Target == null)
        {
            State = AIState.Idle;
            return;
        }

        if (Vector3.Distance(transform.position, Target.transform.position) >= OutOfRangeDistance)
        {
            State = AIState.MovingToAttack;
            return;
        }

        transform.LookAt(Target.transform.position);

        if (_fireDuration > 0f)
        {
            foreach(var weapon in AIWeapons)
                weapon.ShootAt(Target);

            _fireDuration -= Time.deltaTime;
            _fireCoolDown = FireCoolDown;
            return;
        }

        _fireDuration = 0;

        foreach (var weapon in AIWeapons)
            weapon.StopShooting();

        if (_fireCoolDown > 0f)
        {
            _fireCoolDown -= Time.deltaTime;
            return;
        }

        _fireDuration = FireDuration;
    }

    void MovingToAttackUpdate()
    {
        if (Target == null)
        {
            State = AIState.Idle;
            return;
        }

        if (Vector3.Distance(transform.position, Target.transform.position) <= AttackAtDistance)
        {
            State = AIState.Attacking;
            return;
        }

        var direction = (Target.transform.position - transform.position).normalized;

        transform.position += direction * Speed * Time.deltaTime;

        transform.LookAt(Target.transform.position);

    }

    void MovingUpdate()
    {
        if (Vector3.Distance(transform.position, MoveTarget) <= MoveToDistance)
        {
            MoveTarget = Vector3.zero;
            State = AIState.Idle;
            return;
        }

        var direction = (MoveTarget - transform.position).normalized;

        transform.position += direction * Speed * Time.deltaTime;

        transform.LookAt(MoveTarget);
    }

    void SleepUpdate()
    {
        //TODO: When/if plans are added this will allow the ai to sleep for a bit.
    }

}
