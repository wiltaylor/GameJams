using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    public UnityEvent OnFire;
    public UnityEvent OnReleaseFire;

    public void Fire()
    {
        OnFire.Invoke();
    }

    public void ReleaseFire()
    {
        OnReleaseFire.Invoke();
    }

}
