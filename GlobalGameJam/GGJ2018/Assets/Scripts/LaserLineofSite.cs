using System;
using UnityEngine;

public class LaserLineofSite : MonoBehaviour
{
    public enum LaserDirection
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }

    public float CheckTime = 2f;
    public LaserDirection Direction;
    public float Distance;
    private LineRenderer _lineRender;
    private float _timeToNextCheck;

    void Start()
    {
        _lineRender = GetComponent<LineRenderer>();
        _timeToNextCheck = 0;
    }

    public void FixedUpdate()
    {
        if (_timeToNextCheck > 0f)
        {
            _timeToNextCheck -= Time.deltaTime;
            return;
        }

        _timeToNextCheck = CheckTime;

        Ray ray;
        Vector3 DefaultPosition = Vector3.zero;

        switch (Direction)
        {
            case LaserDirection.Up:
                ray = new Ray(transform.position, Vector3.up);
                DefaultPosition = new Vector3(transform.position.x, transform.position.y + Distance, transform.position.z);
                break;
            case LaserDirection.Down:
                ray = new Ray(transform.position, Vector3.down);
                DefaultPosition = new Vector3(transform.position.x, transform.position.y - Distance, transform.position.z);
                break;
            case LaserDirection.Left:
                ray = new Ray(transform.position, Vector3.left);
                DefaultPosition = new Vector3(transform.position.x - Distance, transform.position.y, transform.position.z);
                break;
            case LaserDirection.Right:
                ray = new Ray(transform.position, Vector3.right);
                DefaultPosition = new Vector3(transform.position.x + Distance, transform.position.y, transform.position.z);
                break;
            case LaserDirection.Forward:
                ray = new Ray(transform.position, Vector3.forward);
                DefaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Distance);

                break;
            case LaserDirection.Back:
                ray = new Ray(transform.position, Vector3.back);
                DefaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - Distance);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _lineRender.enabled = true;
            _lineRender.SetPosition(1, hit.point);

            var killtarget = hit.transform.GetComponent<Killable>();

            if (killtarget != null)
            {
                killtarget.Kill();
            }

        }
        else
        {
            _lineRender.SetPosition(1, DefaultPosition);
            _lineRender.enabled = true;
        }
    }
	
}
