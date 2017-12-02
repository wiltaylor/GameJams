using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Damage;

    private Rigidbody _rigidbody;
    public void Shoot(float speed, Vector3 direction)
    {

        //Not done in start for some reason.
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        _rigidbody.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        //do damage

        Destroy(gameObject);
    }


}
