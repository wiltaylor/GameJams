using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject ProjectilePrefab;
    public Rigidbody ridgidbody;
    public float Speed = 10f;
    public float MaxSpeed = 20f;
    public Transform ProjectilePoint;


    private FactoryController _factory;

    void Start()
    {
        _factory = GlobalController.Instance.FactoryController;
        _factory.RegisterType("PlayerProjectile", ProjectilePrefab);
    }

    void Update ()
    {
        //Debug.Log(ridgidbody.velocity);

        if (Input.GetButton("Thrust"))
        {
            if (ridgidbody.velocity.z < MaxSpeed)
            {
                //ridgidbody.AddForce(Vector3.zero, ForceMode.VelocityChange);
                //ridgidbody  AddForce(transform.forward * Speed * Time.deltaTime, ForceMode.VelocityChange);
                ridgidbody.AddRelativeForce(Vector3.forward * Speed * Time.deltaTime, ForceMode.Force);
            }
        }
        else
        {
            ridgidbody.AddForce(Vector3.zero, ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Fire"))
        {

            Debug.Log("Pew pew");
            var projectile = _factory.GetObject("PlayerProjectile");

            projectile.transform.rotation = transform.rotation;
            projectile.transform.position = ProjectilePoint.position;
            projectile.SetActive(true);
        }
	}
}
