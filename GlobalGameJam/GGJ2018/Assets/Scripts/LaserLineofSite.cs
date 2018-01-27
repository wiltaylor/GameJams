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
    private BeamReflector _reflector;

    void Start()
    {
        _lineRender = GetComponent<LineRenderer>();
        _timeToNextCheck = 0;
    }

    void OnDrawGizmosSelected()
    {
        var defaultPosition = Vector3.zero;

        switch (Direction)
        {
            case LaserDirection.Up:
                defaultPosition = new Vector3(transform.position.x, transform.position.y + Distance, transform.position.z);
                break;
            case LaserDirection.Down:
                defaultPosition = new Vector3(transform.position.x, transform.position.y - Distance, transform.position.z);
                break;
            case LaserDirection.Left:
                defaultPosition = new Vector3(transform.position.x - Distance, transform.position.y, transform.position.z);
                break;
            case LaserDirection.Right:
                defaultPosition = new Vector3(transform.position.x + Distance, transform.position.y, transform.position.z);
                break;
            case LaserDirection.Forward:
                defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Distance);

                break;
            case LaserDirection.Back:
                defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - Distance);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, defaultPosition);

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
        var defaultPosition = Vector3.zero;

        switch (Direction)
        {
            case LaserDirection.Up:
                ray = new Ray(transform.position, Vector3.up);
                defaultPosition = new Vector3(transform.position.x, transform.position.y + Distance, transform.position.z);
                break;
            case LaserDirection.Down:
                ray = new Ray(transform.position, Vector3.down);
                defaultPosition = new Vector3(transform.position.x, transform.position.y - Distance, transform.position.z);
                break;
            case LaserDirection.Left:
                ray = new Ray(transform.position, Vector3.left);
                defaultPosition = new Vector3(transform.position.x - Distance, transform.position.y, transform.position.z);
                break;
            case LaserDirection.Right:
                ray = new Ray(transform.position, Vector3.right);
                defaultPosition = new Vector3(transform.position.x + Distance, transform.position.y, transform.position.z);
                break;
            case LaserDirection.Forward:
                ray = new Ray(transform.position, Vector3.forward);
                defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Distance);

                break;
            case LaserDirection.Back:
                ray = new Ray(transform.position, Vector3.back);
                defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - Distance);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _lineRender.enabled = true;
            _lineRender.SetPosition(1, hit.point);
            _lineRender.SetPosition(0, transform.position);

            var killtarget = hit.transform.GetComponent<Killable>();

            if (killtarget != null)
            {
                killtarget.Kill();
            }

            var reflector = hit.transform.GetComponent<BeamReflector>();

            if (reflector == _reflector)
                return;

            if (reflector == null)
            {
                if (_reflector != null)
                    _reflector.OnReset();

                _reflector = null;
                return;
            }

            if(_reflector != null)
                _reflector.OnReset();

            reflector.OnHitByLaser();
            _reflector = reflector;
        }
        else
        {
            if (_reflector != null)
                _reflector.OnReset();

            _reflector = null;
            
            _lineRender.SetPosition(1, defaultPosition);
            _lineRender.SetPosition(0, transform.position);
            _lineRender.enabled = true;
        }
    }
	
}
