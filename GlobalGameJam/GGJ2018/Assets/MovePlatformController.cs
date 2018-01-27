using UnityEngine;

public class MovePlatformController : MonoBehaviour
{
    public NavPoint NextNavPoint;

    public float Distance;
    public float Speed;

    private float _waiting;

    void FixedUpdate()
    {
        if (_waiting > 0f)
        {
            _waiting -= Time.fixedDeltaTime;
            return;
        }

        if (NextNavPoint == null)
            return;

        if (Vector3.Distance(transform.position, NextNavPoint.transform.position) <= Distance)
        {
            _waiting = NextNavPoint.Wait;
            NextNavPoint.OnEnterPoint.Invoke();
            NextNavPoint = NextNavPoint.NextPoint;
            
            return;
        }

        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, NextNavPoint.transform.position, Speed * Time.fixedDeltaTime), transform.rotation);
    }

    public void SetNextNavPoint(NavPoint nav)
    {
        NextNavPoint = nav;
    }

}
