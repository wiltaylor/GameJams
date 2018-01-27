using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeamReflector : MonoBehaviour
{

    private LaserLineofSite _lineofSite;
    private LineRenderer _lineRenderer;
    public UnityEvent OnStartReflect;
    public UnityEvent OnStopReflect;

    private void Start()
    {
        _lineofSite = GetComponent<LaserLineofSite>();
        _lineRenderer = GetComponent<LineRenderer>();
        OnReset();
    }

    public void OnHitByLaser()
    {
        if(_lineofSite != null)
            _lineofSite.enabled = true;
        if (_lineRenderer != null)
            _lineRenderer.enabled = true;
        OnStartReflect.Invoke();
    }

    public void OnReset()
    {
        if (_lineofSite != null)
            _lineofSite.enabled = false;
        if (_lineRenderer != null)
            _lineRenderer.enabled = false;
        OnStopReflect.Invoke();

    }

}
