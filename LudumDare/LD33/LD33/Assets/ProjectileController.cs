using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{

    public float LifeTime = 10f;
    public int Damage = 10;
    public float Velocity = 1f;

    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        Destroy(gameObject, LifeTime);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        _rigidbody2D.MovePosition(new Vector2(transform.position.x + Velocity * Time.fixedDeltaTime, transform.position.y));
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Level")
        {
            Destroy(gameObject);
            return;
        }

        if (col.tag == "Unit" || col.tag == "Hero")
        {
            col.gameObject.SendMessage("TakeHit", Damage);
            Destroy(gameObject);
        }

    }
}
