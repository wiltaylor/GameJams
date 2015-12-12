using UnityEngine;
using System.Collections;

public class ProjectileHandler : MonoBehaviour
{

    public float Duration = 10f;
    public int Damage = 10;
    public float Speed = 100f;

    private float _countdown = 0f;
    private new Rigidbody rigidbody;

    public void Start()
    {
        _countdown = Duration;
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if(rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();

        _countdown = Duration;
        rigidbody.velocity = Vector3.zero;
    }

    void Update()
    {
        _countdown -= Time.deltaTime;

        rigidbody.AddForce(transform.forward * Speed * Time.deltaTime, ForceMode.Force);


        if (_countdown <= 0f)
        {
            Destruct();
        }
    }

    void Destruct()
    {
        _countdown = Duration;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "AI")
            collision.gameObject.SendMessage("Hit", Damage);
        Destruct();
    }
}