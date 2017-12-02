using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    public Vector3 MoveTarget
    {
        get { return _agent.destination; }
    }

    public float DistanceToTarget
    {
        get { return _agent.remainingDistance; }
    }

    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public bool MoveTo(Vector3 point)
    {
        return _agent.SetDestination(point);
    }

    public void ShootAt(Vector3 point)
    {
        transform.LookAt(point);
        Debug.Log("Bang bang");
    }

    public void Die()
    {
        Debug.Log("Die!");
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void LookAt(Vector3 point)
    {
        transform.LookAt(point);
    }
}

