using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public GameObject OrbitPrefab;

    public PlayerEnergyController EnergyController;
    public float MinDistance = 0.5f;
    public float MaxDistance = 1f;
    public float MinSpeed = 100f;
    public float MaxSpeed = 110f;
    public float SpawnTimeOut = 0.5f;

    public float SendToSpeed = 1f;

    private float _currentSpwanTimeOut = 0f;

    private List<Orbiter> _orbiters = new List<Orbiter>();

    void Update()
    {
        if (_currentSpwanTimeOut > 0f)
        {
            _currentSpwanTimeOut -= Time.deltaTime;
            return;
        }

        if (_orbiters.Count < EnergyController.Energy)
        {
            AddOrb();
            _currentSpwanTimeOut = SpawnTimeOut;
        }
    }

    void AddOrb()
    {
        var obj = Instantiate(OrbitPrefab);
        obj.transform.parent = transform;

        var controller = obj.GetComponent<Orbiter>();
        controller.Distance = Random.Range(MinDistance, MaxDistance);
        controller.Speed = Random.Range(MinSpeed, MaxSpeed);
        controller.Target = transform;

        _orbiters.Add(controller);
    }

    public void UseOrb(Transform target)
    {
        var orb = _orbiters.FirstOrDefault();

        if (orb == null)
            return;


        _orbiters.Remove(orb);

        orb.SetTarget(target);
        orb.Speed = SendToSpeed;
        orb.transform.SetParent(null);

    }


}
