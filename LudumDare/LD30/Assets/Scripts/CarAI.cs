using System;
using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour
{
    public GameObject NavNode;
    public float MaxDistanceToNode = 5f;
    public float EnginePower = 10f;

    private Rigidbody2D _rbody;
	void Start ()
	{
	    _rbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
	    var dist = Vector3.Distance(NavNode.transform.position, transform.position);

	    if (dist < MaxDistanceToNode)
	    {
	        NavNode = NavNode.GetComponent<NavNodeController>().NextNode;
            dist = Vector3.Distance(NavNode.transform.position, transform.position);

	    }
        var lookdir = NavNode.transform.position - transform.position;

        _rbody.velocity = lookdir.normalized * EnginePower;
        

	    var angle = Mathf.Atan2(lookdir.y, lookdir.x)*Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(0f, 0f, angle*Mathf.Rad2Deg);

        //_rbody.AddForce(transform.up.normalized * EnginePower, ForceMode2D.Force);

	}

    public void ModEnginePower(float power)
    {
        EnginePower += power;
    }
}
