using System;
using System.Linq;
using UnityEngine;

public enum AIState
{
    Idle,
    Attack
}

public class AttackAI : MonoBehaviour
{
    public Vector3 LastKnownPlayer = Vector3.zero;
    public AIState State;
    public float ShootCoolDown;
    public Transform Target;
    public float SightDistance;
    public float StopingDistance = 0.1f;
    public LayerMask DetectionMask;

    private float _shootCoolDown = 0f;
    private AIControl _aiControl;

    void Start()
    {
        _aiControl = GetComponent<AIControl>();
    }

    bool DetectPlayer()
    {
        //Checking if player is in range.
        var player = Physics.OverlapSphere(transform.position, SightDistance).FirstOrDefault(c => c.CompareTag("Player"));

        if (player == null) return false;

        //Checking if there is a line of sight on the player.
        RaycastHit hit;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        if (!Physics.Raycast(transform.position, direction, out hit, SightDistance))
            return false;

        Target = player.transform;
        return true;
    }


    void Update()
    {
        switch (State)
        {
            case AIState.Idle:

                if (DetectPlayer())
                {
                    State = AIState.Attack;
                    
                }
                break;
            case AIState.Attack:
                if (_shootCoolDown > 0f)
                {
                    _shootCoolDown -= Time.deltaTime;
                    _aiControl.LookAt(Target.position);
                    break;
                }

                RaycastHit hit;
                Vector3 direction = (Target.position - transform.position).normalized;

                if (Physics.Raycast(transform.position, direction, out hit, SightDistance))
                {
                    if (hit.transform != Target)
                    {
                        LastKnownPlayer = Target.position;
                        State = AIState.Idle;
                        
                        break;
                    }
                    _aiControl.ShootAt(Target.position);

                    _shootCoolDown = ShootCoolDown;
                    break;
                }
                State = AIState.Idle;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
