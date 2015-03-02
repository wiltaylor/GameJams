using System;
using UnityEngine;
using System.Collections;

public class EmpireController : MonoBehaviour
{
    public GameObject CurrentPlanet;
    public int Troops = 1000;
    public int Ships = 100;
    public int Food = 1000;
    public int Fuel = 1000;

    public float FuelPerUnit = 0.1f;
    public float FoodPerUnit = 0.1f;
    public float FuelToSpyPerUnit = 0.01f;
    
    public void MoveToPlanet(GameObject target)
    {
        if (CurrentPlanet != null)
        {
            Food -= FoodToFlyTo(target);
            Fuel -= FuelToFlyTo(target);
            GameManager.Instance.PassTime(YearsToFlyTo(target));
        }

        transform.position = target.transform.position;
        CurrentPlanet = target;
    }

    public int FoodToFlyTo(GameObject planet)
    {
        var distance = Vector3.Distance(CurrentPlanet.transform.position, planet.transform.position);
        var fleetUsagePerUnit = Ships*FuelPerUnit;
        return (int)Math.Floor(distance*fleetUsagePerUnit);
    }

    public int FuelToFlyTo(GameObject planet)
    {
        var distance = Vector3.Distance(CurrentPlanet.transform.position, planet.transform.position);
        var fleetUsagePerUnit = Troops * FoodPerUnit;
        return (int)Math.Floor(distance * fleetUsagePerUnit);
    }

    public int FuelToSpy(GameObject planet)
    {
        var distance = Vector3.Distance(CurrentPlanet.transform.position, planet.transform.position);
        return (int) Math.Floor(distance*FuelToSpyPerUnit);
    }

    public int YearsToFlyTo(GameObject planet)
    {
        var distance = Vector3.Distance(CurrentPlanet.transform.position, planet.transform.position);
        return (int)Math.Floor(distance);
    }
}
