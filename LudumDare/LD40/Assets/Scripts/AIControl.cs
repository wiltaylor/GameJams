
using Boo.Lang;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public ItemPickup[] DropList;
    public GameObject WeaponSlot;
    public WeaponItem Weapon;
    public float DeathTimeOut = 5f;
    public ParticleSystem MeatFountain;
    private Transform _bulletPoint;
    private Vector3 _shootTarget;
    public AudioSource Sighted;

    public Vector3 MoveTarget
    {
        get { return _agent.destination; }
    }

    public void PlaySighted()
    {
        if(Sighted != null)
            Sighted.Play();
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
        _shootTarget = point;


        _animator.SetTrigger("Shoot");
    }

    public void FireProjectile()
    {
        _bulletPoint.LookAt(_shootTarget);


        var bullet = Instantiate(Weapon.Projectile);
        var controller = bullet.GetComponent<Projectile>();
        controller.transform.position = _bulletPoint.position;
        controller.Damage = Weapon.Damage;
        controller.Shoot(Weapon.ProjectileSpeed, _bulletPoint.forward);
    }

    public void Die()
    {
        Destroy(gameObject, DeathTimeOut);
        MeatFountain.gameObject.SetActive(true);
        foreach (var c in GetComponentsInChildren<BoxCollider>())
            c.enabled = false;

        _agent.enabled = false;
        _animator.ResetTrigger("Shoot");

        GetComponent<AIAnimation>().enabled = false;

        GetComponent<AttackAI>().enabled = false;
        //enabled = false;

        _animator.SetTrigger("Dead");

        if(DropList != null)
            foreach (var item in DropList)
            {
                var obj = Instantiate(item);
                obj.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            }
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

