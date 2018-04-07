using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeamWeaponController : MonoBehaviour
{

    public Color BeamColour;
    public float distance;
    public LineRenderer LeftBeam;
    public LineRenderer RightBeam;
    public ParticleSystem LeftParticle;
    public ParticleSystem RightParticle;
    public float BeamDamage = 1f;
    public bool BeamPunchThrough = false;
    public GameObject Owner;

    private Vector3 _leftEndVector;
    private Vector3 _rightEndVector;

    public void EnableBeam(bool active)
    {
        _beamOn = active;

        LeftBeam.gameObject.SetActive(_beamOn);
        RightBeam.gameObject.SetActive(_beamOn);

        if (!_beamOn)
        {
            RightParticle.gameObject.SetActive(false);
            LeftParticle.gameObject.SetActive(false);
        }
    }

    private bool _beamOn;

    public void ResetBeam()
    {
        LeftBeam.positionCount = 2;
        RightBeam.positionCount = 2;

        _leftEndVector = new Vector3(0, 0, distance);
        _rightEndVector = new Vector3(0,0, distance);

        LeftBeam.SetPosition(0, new Vector3(-2, 0, 0));
        LeftBeam.SetPosition(1, _leftEndVector);

        RightBeam.SetPosition(0, new Vector3(2, 0, 0));
        RightBeam.SetPosition(1, _rightEndVector);

        LeftBeam.startColor = BeamColour;
        LeftBeam.endColor = BeamColour;


        RightBeam.startColor = BeamColour;
        RightBeam.endColor = BeamColour;

        var leftMain = LeftParticle.main;
        var rightMain = RightParticle.main;

        leftMain.startColor = new ParticleSystem.MinMaxGradient(BeamColour);
        rightMain.startColor = new ParticleSystem.MinMaxGradient(BeamColour);
    }

    void Start()
    {
        ResetBeam();
    }

    void FixedUpdate()
    {
        if (!_beamOn)
            return;

        Vector3 leftHit = Vector3.zero;
        Vector3 rightHit = Vector3.zero;
        
        var leftBeamStart = transform.TransformPoint(LeftBeam.GetPosition(0));
        var leftBeamEnd = transform.TransformPoint(_leftEndVector);

        var rightBeamStart = transform.TransformPoint(RightBeam.GetPosition(0));
        var rightBeamEnd = transform.TransformPoint(_leftEndVector);

        var leftDirection = (leftBeamEnd - leftBeamStart).normalized;
        var rightDirection = (rightBeamEnd - rightBeamStart).normalized;
        
        var leftRay = new Ray(leftBeamStart, leftDirection);
        var rightRay = new Ray(rightBeamStart, rightDirection);

        var lefthit = Physics.RaycastAll(leftRay, distance).OrderBy(h => h.distance);
        var righthit = Physics.RaycastAll(rightRay, distance).OrderBy(h => h.distance);
        
        foreach (var hit in lefthit)
        {
            var dmg = hit.transform.GetComponent<Destructable>();

            if (dmg != null)
                dmg.HP -= BeamDamage * Time.fixedDeltaTime;

            if (leftHit == Vector3.zero)
            {
                leftHit = transform.InverseTransformPoint(hit.point);
            }

            if (!BeamPunchThrough)
                break;
        }

        foreach (var hit in righthit)
        {
            var dmg = hit.transform.GetComponent<Destructable>();

            if (dmg != null)
                dmg.DoHit(BeamDamage, Owner);
            
            if (rightHit == Vector3.zero)
            {
                rightHit = transform.InverseTransformPoint(hit.point);
            }

            if (!BeamPunchThrough)
                break;
        }

        LeftBeam.SetPosition(1, leftHit != Vector3.zero ? leftHit : _leftEndVector);
        RightBeam.SetPosition(1, rightHit != Vector3.zero ? rightHit : _rightEndVector);

        LeftParticle.gameObject.SetActive(leftHit != Vector3.zero);
        LeftParticle.transform.position = transform.TransformPoint(leftHit);
        RightParticle.gameObject.SetActive(rightHit != Vector3.zero);
        RightParticle.transform.position = transform.TransformPoint(rightHit);

    }
}
