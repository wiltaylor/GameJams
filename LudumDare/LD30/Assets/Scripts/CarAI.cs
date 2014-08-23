using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour
{
    public GameObject NavNode;
    public float MaxDistanceToNode = 5f;
    public float EnginePower = 10f;
    public float StunTimeOut = 10f;

    private float _stuneTimeOut = 0f;
    private Vector3 _targetLocation;
    private AudioSource _audio;
    private Animator _animator;

    private Rigidbody2D _rbody;
	void Start ()
	{
	    _rbody = gameObject.GetComponent<Rigidbody2D>();
        _targetLocation = new Vector2(NavNode.transform.position.x, NavNode.transform.position.y) + Random.insideUnitCircle * 5;
	    _audio = GetComponent<AudioSource>();
	    _animator = GetComponent<Animator>();

	    _audio.pitch = Random.Range(0f, 1f);
	}

    void OnProjectileHit()
    {
        _stuneTimeOut = StunTimeOut;
    }
	
	void FixedUpdate ()
	{
        _animator.SetFloat("Speed", _rbody.velocity.magnitude);

	    if (_rbody.velocity.magnitude < 0.1)
	    {
            if (_audio.isPlaying)
	            _audio.Stop();
	    }
	    else
	    {
	        if(!_audio.isPlaying)
                _audio.Play();
	    }

	    if (_stuneTimeOut > 0)
	    {
	        _stuneTimeOut -= Time.fixedDeltaTime;
	        return;
	    }

        var dist = Vector3.Distance(_targetLocation, transform.position);

	    if (dist < MaxDistanceToNode)
	    {
	        NavNode = NavNode.GetComponent<NavNodeController>().NextNode;
            _targetLocation = new Vector2(NavNode.transform.position.x, NavNode.transform.position.y) + Random.insideUnitCircle * 5;

	    }
        var lookdir = _targetLocation - transform.position;

        var rotation = Quaternion.LookRotation(_targetLocation - transform.position,
	        transform.TransformDirection(Vector3.back));
        transform.rotation = new Quaternion(0,0, rotation.z, rotation.w);

        

        _rbody.AddForce(transform.up.normalized * EnginePower, ForceMode2D.Force);
	}

    public void ModEnginePower(float power)
    {
        EnginePower += power;
    }
}
