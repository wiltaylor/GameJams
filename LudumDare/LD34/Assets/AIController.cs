using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public GameObject Target;
    public float ShootCoolDown = 5;
    public GameObject ProjectilePrefab;

    private float _currentCoolDown = 0;
    private FactoryController _factory;
    public Transform ProjectilePoint;

    void Start ()
    {
        _factory = GlobalController.Instance.FactoryController;
        _factory.RegisterType("AIProjectile", ProjectilePrefab);
    }
	
	void Update ()
    {
        if (Target == null)
            return;

        transform.LookAt(Target.transform);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        if (_currentCoolDown <= 0)
        {
            _currentCoolDown = ShootCoolDown;

            var projectile = _factory.GetObject("AIProjectile");

            projectile.transform.rotation = transform.rotation;
            projectile.transform.position = ProjectilePoint.position;
            projectile.SetActive(true);
        }
        else
        {
            _currentCoolDown -= Time.deltaTime;
        }
	}

    void Hit(int damage)
    {
        Destroy(gameObject);
    }
}
