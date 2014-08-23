using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{

    private Rigidbody2D _rbody;
    public float EnginePower = 1f;
    public float BrakePower = 0.5f;
    public float TurnSpeed = 10f;
    public float StunTimeout = 10f;
    private Animator _animator;
    private PowerUpCollector _powerCollector;
    private float _stunTimeOut = 0f;
    private AudioSource _audio;

	// Use this for initialization
	void Start ()
	{
	    _rbody = gameObject.rigidbody2D;
	    _animator = gameObject.GetComponent<Animator>();
	    _powerCollector = gameObject.GetComponent<PowerUpCollector>();
        _audio = GetComponent<AudioSource>();
        _audio.pitch = Random.Range(0f, 1f);
	}

    void OnProjectileHit()
    {
        _stunTimeOut = StunTimeout;
    }
	
	// Update is called once per frame
	void Update () {
        _animator.SetFloat("Speed", _rbody.velocity.magnitude);

        if (_rbody.velocity.magnitude < 0.1)
        {
            if (_audio.isPlaying)
                _audio.Stop();
        }
        else
        {
            if (!_audio.isPlaying)
                _audio.Play();
        }


	    if (_stunTimeOut > 0)
	    {
	        _stunTimeOut -= Time.deltaTime;
	        return;
	    }

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
	}

    public void ModEnginePower(float power)
    {
        EnginePower += power;
    }
}
