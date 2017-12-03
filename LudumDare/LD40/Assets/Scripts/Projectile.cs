using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float Damage;
    public float TimeOut = 20f;
    public AudioSource ShootSound;

    private Rigidbody _rigidbody;
    public void Shoot(float speed, Vector3 direction)
    {

        //Not done in start for some reason.
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        _rigidbody.velocity = direction * speed;

        if(ShootSound != null)
            ShootSound.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        var hitbox = collision.transform.GetComponent<HitBox>();

        if (hitbox != null)
        {
            hitbox.DoDamage(Damage);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        TimeOut -= Time.deltaTime;

        if(TimeOut < 0f)
            Destroy(gameObject);
    }
}
