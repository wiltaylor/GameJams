using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimation : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    
	void Start ()
	{
	    _agent = GetComponent<NavMeshAgent>();
	    _animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
		_animator.SetFloat("Speed", _agent.velocity.magnitude);
	}
}
