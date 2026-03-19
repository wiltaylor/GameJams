using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIWeaponController : MonoBehaviour
{
    public Color Colour;
    public LineRenderer Beam;
    public float Distance;
    public float Damage;

    void ResetBeam(Vector3 point)
    {
        Beam.positionCount = 2;
        Beam.SetPosition(0, transform.position);
        Beam.SetPosition(1, point);

        Beam.startColor = Colour;
        Beam.endColor = Colour;
    }

    public void StopShooting()
    {
        Beam.positionCount = 0;
    }

    public void ShootAt(GameObject target)
    {
        ResetBeam(target.transform.position);

        var dmg = target.GetComponent<Destructable>();

        if (dmg == null)
            return;

        dmg.DoHit(Damage, transform.parent.gameObject);
        
    }

}
