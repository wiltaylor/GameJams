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
    public float RotSpeed = 10f;
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
    private AudioSource _audio;


    void Update()
    {
        foreach (var weapon in AIWeapons)
            weapon.StopShooting();

        switch (State)
        {
            case AIState.Idle:
                _audio.Stop();
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
        _audio = GetComponent<AudioSource>();

    }

    public void OnShot()
    {
        if (_destructable.LastHitBy == null)
            return;

        Target = _destructable.LastHitBy;
        State = AIState.MovingToAttack;
    }

    void RotatingToAttackUpdate()
    {
        
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

        //transform.LookAt(Target.transform.position);

        var dir = (Target.transform.position - transform.position).normalized;
        var rot = Quaternion.FromToRotation(transform.forward, dir);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * RotSpeed);


        if (_fireDuration > 0f)
        {
            _audio.Play();
            foreach (var weapon in AIWeapons)
                weapon.ShootAt(Target);

            _fireDuration -= Time.deltaTime;
            _fireCoolDown = FireCoolDown;
            return;
        }

        _fireDuration = 0;
        _audio.Stop();

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

        var dir = (Target.transform.position - transform.position).normalized;
        var rot = Quaternion.FromToRotation(transform.forward, dir);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * RotSpeed);
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

    void OnTriggerEnter(Collider other)
    {
        if (Target != null)
            return;

        var dmg = other.GetComponent<Destructable>();

        if (dmg == null)
            return;

        if (dmg.Faction == DestructableFaction.None || dmg.Faction == _destructable.Faction)
            return;

        Target = other.gameObject;
        State = AIState.MovingToAttack;
    }

}
