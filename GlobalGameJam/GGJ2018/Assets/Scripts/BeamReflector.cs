using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflector : MonoBehaviour
{

    private LaserLineofSite _lineofSite;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineofSite = GetComponent<LaserLineofSite>();
        _lineRenderer = GetComponent<LineRenderer>();
        OnReset();
    }

    public void OnHitByLaser()
    {
        _lineofSite.enabled = true;
        _lineRenderer.enabled = true;
    }

    public void OnReset()
    {
        _lineofSite.enabled = false;
        _lineRenderer.enabled = false;

    }

}
