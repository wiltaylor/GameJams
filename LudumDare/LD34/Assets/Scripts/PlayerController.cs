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

        
        if (Input.GetAxis("Vertical") > 0f & ridgidbody.velocity.magnitude < MaxSpeed)
        {
            ridgidbody.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * Speed * Time.deltaTime, ForceMode.Force);
        }
        else if(Input.GetAxis("Vertical") < 0f)
        {
            ridgidbody.AddForce(new Vector3(transform.forward.x, 0, transform.forward.z) * Speed * Time.deltaTime * -1, ForceMode.Force);
        }

        if(Input.GetAxis("Horizontal") > 0f)
        {
            ridgidbody.AddForce(new Vector3(transform.right.x, 0, transform.right.z) * Speed * Time.deltaTime, ForceMode.Force);
        }
        else if(Input.GetAxis("Horizontal") < 0f)
        {
            ridgidbody.AddForce(new Vector3(transform.right.x, 0, transform.right.z) * Speed * Time.deltaTime * -1, ForceMode.Force);
        }

        if (Input.GetButtonDown("Fire"))
        {
            var projectile = _factory.GetObject("PlayerProjectile");

            projectile.transform.rotation = transform.rotation;
            projectile.transform.position = ProjectilePoint.position;
            projectile.SetActive(true);
        }
	}

    void Hit(int damage)
    {
        Debug.Log("Ow!");
    }
}
