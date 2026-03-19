using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public GameObject Target;
    public float ShootCoolDown = 5;
    public GameObject ProjectilePrefab;
    public GameObject DeadPrefab;
    public float DeathCooldown = 10f;
    public float DeathExplosionForce = 1000f;
    public float DeathExplosionSize = 100f;
    public float Range = 20f;

    private float _currentCoolDown = 0;
    private FactoryController _factory;
    private bool _dead = false;
    public Transform ProjectilePoint;

    void Start ()
    {
        _factory = GlobalController.Instance.FactoryController;
        _factory.RegisterType("AIProjectile", ProjectilePrefab);
    }
	
	void Update ()
    {
        if (GlobalController.Instance.GamePaused)
            return;

        if (Target == null)
            return;

        transform.LookAt(Target.transform);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        if (_currentCoolDown <= 0)
        {
            _currentCoolDown = ShootCoolDown;
            
            var projectile = _factory.GetObject("AIProjectile");

            projectile.transform.position = ProjectilePoint.position;
            projectile.transform.LookAt(Target.transform);
            projectile.SetActive(true);

        }
        else
        {
            _currentCoolDown -= Time.deltaTime;
        }
	}

    void Destruct()
    {
        if (_dead)
            return;

        Destroy(gameObject);
        var dead = (GameObject)(Instantiate(DeadPrefab, transform.position, transform.rotation));

        Destroy(dead, DeathCooldown);

        var rig = dead.GetComponent<Rigidbody>();
        rig.AddExplosionForce(DeathExplosionForce, dead.transform.position, DeathExplosionSize);

        _dead = true;

    }

    void Hit(int damage)
    {
        Destruct();
    }
}
