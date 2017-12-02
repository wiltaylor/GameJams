using Microsoft.Win32;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public GameObject WeaponSlot;
    public WeaponItem Weapon;


    private Transform _bulletPoint;

    public Vector3 MoveTarget
    {
        get { return _agent.destination; }
    }

    public float DistanceToTarget
    {
        get { return _agent.remainingDistance; }
    }

    private NavMeshAgent _agent;
    private Animator _animator;
    

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        var gun = Instantiate(Weapon.AIGun);
        gun.transform.SetParent(WeaponSlot.transform);
        gun.transform.localRotation = Quaternion.identity;
        gun.transform.localPosition = Vector3.zero;

        _bulletPoint = gun.GetComponent<AiGun>().BulletPoint.transform;
    }

    public bool MoveTo(Vector3 point)
    {
        return _agent.SetDestination(point);
    }

    public void ShootAt(Vector3 point)
    {
        transform.LookAt(point);
        _animator.SetTrigger("Shoot");
    }

    public void FireProjectile()
    {
        var bullet = Instantiate(Weapon.Projectile);
        var controller = bullet.GetComponent<Projectile>();
        controller.transform.position = _bulletPoint.position;
        controller.Damage = Weapon.Damage;
        controller.Shoot(Weapon.ProjectileSpeed, transform.forward);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void LookAt(Vector3 point)
    {
        transform.LookAt(point);
    }
}

