using System;
using UnityEngine;

public class LaserLineofSite : MonoBehaviour
{

    public float CheckTime = 2f;
    public float Distance;
    public bool AlwaysRun = false;
    private LineRenderer _lineRender;
    private float _timeToNextCheck;
    private BeamReflector _reflector;

    void Start()
    {
        _lineRender = GetComponent<LineRenderer>();
        _timeToNextCheck = 0;

        _lineRender.positionCount = 2;
    }

    void OnDrawGizmosSelected()
    {
        var defaultPosition = transform.position + (transform.up * Distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, defaultPosition);

    }

    public void FixedUpdate()
    {
        

        if (!AlwaysRun && _timeToNextCheck > 0f)
        {
            _timeToNextCheck -= Time.deltaTime;
            return;
        }

        _timeToNextCheck = CheckTime;

        Ray ray = new Ray(transform.position, transform.up);
        var defaultPosition = transform.position + (transform.up * Distance);

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
