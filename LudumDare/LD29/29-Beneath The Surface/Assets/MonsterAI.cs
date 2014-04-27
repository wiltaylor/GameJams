using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    public float MoveForce =1f;
    public float WaitOnHit = 5f;
    

    private GameObject _player;
    private float _timeOut = 0f;

	// Use this for initialization
	void Start ()
	{
	    _player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (!_player.activeInHierarchy)
	        return;

	    if (_timeOut > 0f)
	    {
	        _timeOut -= Time.fixedDeltaTime;
	        return;
	    }

	    Vector2 direction = (_player.transform.position - transform.position).normalized;
        rigidbody2D.velocity = direction * MoveForce;
        
	}

    public void DoTimeOut()
    {
        _timeOut = WaitOnHit;
    }
}
