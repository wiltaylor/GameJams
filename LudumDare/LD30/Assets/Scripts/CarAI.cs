using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour
{
    public GameObject NavNode;
    public float MaxDistanceToNode = 5f;
    public float Speed = 1f;

    private Rigidbody2D rbody;
	void Start ()
	{
	    rbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
	    var dist = Vector3.Distance(NavNode.transform.position, transform.position);

	    if (dist < MaxDistanceToNode)
	    {
	        NavNode = NavNode.GetComponent<NavNodeController>().NextNode;
            dist = Vector3.Distance(NavNode.transform.position, transform.position);
	    }



	    rbody.velocity = (NavNode.transform.position - transform.position).normalized*Speed;


	}
}
