using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{

    private Rigidbody2D _rbody;
    public GameObject NavNode;
    public float MaxDistanceToNode = 5f;
    public float EnginePower = 1f;
    public float BrakePower = 0.5f;
    public float TurnSpeed = 10f;
    public float StunTimeout = 10f;
    public AudioClip HitSound;
    private Animator _animator;
    private PowerUpCollector _powerCollector;
    private float _stunTimeOut = 0f;
    private AudioSource _audio;
    private CarRankTracker _rankTracker;
    private ParticleSystem _particleSystem;
    private bool _leftDown = false;
    private bool _rightDown = false;
    private bool _upDown = false;
    private bool _downDown = false;



	// Use this for initialization
	void Start ()
	{
	    _rbody = gameObject.rigidbody2D;
	    _animator = gameObject.GetComponent<Animator>();
	    _powerCollector = gameObject.GetComponent<PowerUpCollector>();
        _audio = GetComponent<AudioSource>();
        _audio.pitch = Random.Range(0f, 1f);
        _rankTracker = GetComponent<CarRankTracker>();

	    _particleSystem = GetComponent<ParticleSystem>();
	}

    void OnProjectileHit()
    {
        _stunTimeOut = StunTimeout;
        _particleSystem.Play();
        
        AudioSource.PlayClipAtPoint(HitSound, transform.position);
    }

    void FixedUpdate()
    {
        if (_upDown)
        {
            _rbody.AddForce(transform.up.normalized * EnginePower, ForceMode2D.Force);
        }

        if (_downDown)
        {
            _rbody.AddForce(-transform.up.normalized * BrakePower, ForceMode2D.Force);
        }

        if (_leftDown)
        {
            _rbody.MoveRotation(_rbody.rotation + TurnSpeed * Time.deltaTime);
        }

        if (_rightDown)
        {
            _rbody.MoveRotation(_rbody.rotation - TurnSpeed * Time.deltaTime);
        }
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

            _particleSystem.Stop();
        }


	    if (_stunTimeOut > 0)
	    {
	        _stunTimeOut -= Time.deltaTime;
	        return;
	    }

        var dist = Vector3.Distance(NavNode.transform.position, transform.position);

        if (dist < MaxDistanceToNode)
        {
            NavNode = NavNode.GetComponent<NavNodeController>().NextNode;
            dist = Vector3.Distance(NavNode.transform.position, transform.position);
        }

	    _rankTracker.NodeDistance = dist;
	    _rankTracker.NodeID = NavNode.GetComponent<NavNodeController>().NodeID;
	    _rankTracker.NextNode = NavNode;

	    _upDown = Input.GetAxis("Vertical") > 0f;

	    _downDown = Input.GetAxis("Vertical") < 0f;

        _leftDown = Input.GetAxis("Horizontal") < 0f;

	    _rightDown = Input.GetAxis("Horizontal") > 0f;

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
