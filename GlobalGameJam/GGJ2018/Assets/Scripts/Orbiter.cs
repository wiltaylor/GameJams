using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{

    public Transform Target;
    public float Speed = 50f;
    public float Distance = 0.5f;
    public float DestroyDistance = 0.1f;

    private bool _inTargetMode;
   

    void Update()
    {
        if (!_inTargetMode)
        {
            transform.RotateAround(Target.position, Vector3.up, Speed * Time.deltaTime);
            var desiredPosition = (transform.position - Target.position).normalized * Distance + Target.position;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * Speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * Speed);
            var dist = Vector3.Distance(transform.position, Target.position);

            if(dist <= DestroyDistance)
                Destroy(gameObject);
        }


    }

    public void SetTarget(Transform target)
    {
        _inTargetMode = true;
        Target = target;
    }

}
