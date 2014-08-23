using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{

    private Rigidbody2D _rbody;
    public float EnginePower = 1f;
    public float BrakePower = 0.5f;
    public float TurnSpeed = 10f;
    private Animator _animator;
    private PowerUpCollector _powerCollector;

	// Use this for initialization
	void Start ()
	{
	    _rbody = gameObject.rigidbody2D;
	    _animator = gameObject.GetComponent<Animator>();
	    _powerCollector = gameObject.GetComponent<PowerUpCollector>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetAxis("Vertical") > 0f)
	    {
            _rbody.AddForce(transform.up.normalized * EnginePower, ForceMode2D.Force);
	    }

        if (Input.GetAxis("Vertical") < 0f)
        {
            _rbody.AddForce(-transform.up.normalized * BrakePower, ForceMode2D.Force);
        }

	    if (Input.GetAxis("Horizontal") < 0f)
	    {
            _rbody.MoveRotation(_rbody.rotation + TurnSpeed * Time.deltaTime);
	    }

        if (Input.GetAxis("Horizontal") > 0f)
        {
            _rbody.MoveRotation(_rbody.rotation - TurnSpeed * Time.deltaTime);
        }

	    if (Input.GetButton("Powerup"))
	    {
	       _powerCollector.UseItem();
	    }

        _animator.SetFloat("Speed", _rbody.velocity.magnitude);
	}

    public void ModEnginePower(float power)
    {
        EnginePower += power;
    }
}
