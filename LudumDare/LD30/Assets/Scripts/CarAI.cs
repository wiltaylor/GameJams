using System.Linq.Expressions;
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
    private PowerUpCollector _collector;
    private CarRankTracker _rankTracker;

    private Rigidbody2D _rbody;
	void Start ()
	{
	    _rbody = gameObject.GetComponent<Rigidbody2D>();
        _targetLocation = new Vector2(NavNode.transform.position.x, NavNode.transform.position.y) + Random.insideUnitCircle * 5;
	    _audio = GetComponent<AudioSource>();
	    _animator = GetComponent<Animator>();
        _collector = GetComponent<PowerUpCollector>();
	    _rankTracker = GetComponent<CarRankTracker>();

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

	    if (_collector.CurrentItem != null)
	    {
	        var Item = _collector.CurrentItem.GetComponent<PowerUpInfo>();
	        switch (Item.Type)
	        {
	            case PowerUpInfo.ItemType.Buff:
                    _collector.UseItem();
	                break;
                case PowerUpInfo.ItemType.ProjectileBack:
	                break;
                case PowerUpInfo.ItemType.ProjectileFoward:
                    if (CanSeeCar())
                        _collector.UseItem();
	                break;

	        }
	    }

        var dist = Vector3.Distance(_targetLocation, transform.position);

	    if (dist < MaxDistanceToNode)
	    {
	        NavNode = NavNode.GetComponent<NavNodeController>().NextNode;
            dist = Vector3.Distance(NavNode.transform.position, transform.position);
            _targetLocation = new Vector2(NavNode.transform.position.x, NavNode.transform.position.y) + Random.insideUnitCircle * 5;

	    }

        _rankTracker.NodeDistance = dist;
        _rankTracker.NodeID = NavNode.GetComponent<NavNodeController>().NodeID;
	    _rankTracker.NextNode = NavNode;

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

    bool CanSeeCar()
    {
        var hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), 5f, Vector2.up);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
                continue;

            if (hit.collider.tag != "Car")
                continue;

            return true;
        }

        return false;
    }
}
