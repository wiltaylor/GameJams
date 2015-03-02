using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Permissions;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{

    public GameObject EmpireObject;
    public GameObject SelectorObject;
    public GameObject PlanetPrefab;
    public GameObject GameMap;
    public Vector3 HomePlanetLocation;
    public float MaxX = 5000f;
    public float MaxY = 5000f;
    public float MinX = -5000f;
    public float MinY = -5000f;
    public int NumberOfPlanets = 100;
    public float MinDistance = 5f;

    public int MaxFoodProductionRate = 10;
    public int MinFoodProductionRate = 1;
    public int MaxFuelProductionRate = 10;
    public int MinFuelProductionRate = 1;
    public int MaxTroopProductionRate = 10;
    public int MinTroopProductionRate = 1;
    public int MaxShipProductionRate = 10;
    public int MinShipProductionRate = 1;

    public int MinStartTroops = 10;
    public int MaxStartTroops = 1000;
    public int MaxStartShips = 1000;
    public int MinStartShips = 10;
    public int MinStartFuel = 0;
    public int MaxStartFuel = 100;
    public int MinStartFood = 10;
    public int MaxStartFood = 100;

    public int EmpireStartFood = 10000;
    public int EmpireStartFuel = 10000;
    public int EmpireStartTroops = 1000;
    public int EmpireStartShips = 400;

    public int StartYear = 0;

    public void GenerateLevel()
    {
        ClearLevel();

        var planets = new List<GameObject>();

        var homePlanet = (GameObject)Instantiate(PlanetPrefab);
        
        homePlanet.GetComponent<RectTransform>().position = HomePlanetLocation;
        homePlanet.transform.SetParent(GameMap.transform);

        var homePlanetController = homePlanet.GetComponent<PlanetController>();
        GameManager.Instance.PlanetDirectory.Add(homePlanetController);
        homePlanetController.PlanetName = "Imperium Prime";
        homePlanetController.PlayerOwned = true;
        homePlanetController.Food = MaxStartFood;
        homePlanetController.Troops = MaxStartTroops;
        homePlanetController.Fuel = MaxStartFuel;
        homePlanetController.Ships = MaxStartShips;
        homePlanetController.Food = MaxStartFood;
        homePlanetController.UpdateOwner();
        homePlanetController.FoodProductionRate = MaxFoodProductionRate;
        homePlanetController.FuelProductionRate = MaxFuelProductionRate;
        homePlanetController.TroopProductionRate = MaxTroopProductionRate;
        homePlanetController.ShipProductionRate = MaxShipProductionRate;

        var empire = EmpireObject.GetComponent<EmpireController>();
        empire.MoveToPlanet(homePlanet);
        empire.Food = EmpireStartFood;
        empire.Fuel = EmpireStartFuel;
        empire.Troops = EmpireStartTroops;
        empire.Ships = EmpireStartShips;
        
        SelectorObject.GetComponent<SelectorController>().SelectPlanet(homePlanet);
        planets.Add(homePlanet);

        GameManager.Instance.Year = StartYear;

        for (var i = 0; i < NumberOfPlanets; i++)
        {
            var newPlanet = (GameObject)Instantiate(PlanetPrefab);
            newPlanet.transform.SetParent(GameMap.transform);
            var newPlanetRect = newPlanet.GetComponent<RectTransform>();
            
            var controller = newPlanet.GetComponent<PlanetController>();
            GameManager.Instance.PlanetDirectory.Add(controller);
            controller.PlanetName = GeneratePlanetName();
            controller.Food = Random.Range(MinStartFood, MaxStartFood);
            controller.Troops = Random.Range(MinStartTroops, MaxStartTroops);
            controller.Fuel = Random.Range(MinStartFuel, MaxStartFuel);
            controller.Ships = Random.Range(MinStartShips, MaxStartShips);
            controller.Food = Random.Range(MinStartFood, MaxStartFood);

            controller.FoodProductionRate = Random.Range(MinFoodProductionRate, MaxFoodProductionRate);
            controller.FuelProductionRate = Random.Range(MinFuelProductionRate, MaxFuelProductionRate);
            controller.TroopProductionRate = Random.Range(MinTroopProductionRate, MaxTroopProductionRate);
            controller.ShipProductionRate = Random.Range(MinShipProductionRate, MaxShipProductionRate);
            
            while (true)
            {
                newPlanetRect.position = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
                
                var planetColision = false;
                
                foreach (var p in planets)
                {
                    if (!(Vector3.Distance(newPlanet.transform.position, p.transform.position) < MinDistance)) continue;

                    planetColision = true;
                    break;
                }

                if (planetColision) continue;
                break;
            }

            planets.Add(newPlanet);
        }

    }

    public void ClearLevel()
    {
        GameManager.Instance.PlanetDirectory = new List<PlanetController>();

        var children = new List<GameObject>();

        for (var i = 0; i < GameMap.transform.childCount; i++)
        {
            children.Add(GameMap.transform.GetChild(i).gameObject);
        }

        foreach (var item in children)
        {
            if(item.name == "Empire" || item.name == "Selector")
                continue;
            
            Destroy(item);
        }
    }

    public string GeneratePlanetName()
    {
        var name = "";

        switch (Random.Range(0,10))
        {
            case 0:
                name += "Gala";
                break;
            case 1:
                name += "Gorg";
                break;
            case 2:
                name += "Reth";
                break;
            case 3:
                name += "Sigma";
                break;
            case 4:
                name += "Terra";
                break;
            case 5:
                name += "Xenu";
                break;
            case 6:
                name += "Mega";
                break;
            case 7:
                name += "Etern";
                break;
            case 8:
                name += "Sata";
                break;
            case 9:
                name += "Qura";
                break;
            case 10:
                name += "Phyius";
                break;
        }

        switch (Random.Range(0, 10))
        {
            case 0:
                name += "tron";
                break;
            case 1:
                name += "nova";
                break;
            case 2:
                name += "thion";
                break;
            case 3:
                name += "topia";
                break;
            case 4:
                name += "illion";
                break;
            case 5:
                name += "Ragha";
                break;
            case 6:
                name += "tregeth";
                break;
            case 7:
                name += "yilion";
                break;
            case 8:
                name += "Trag";
                break;
            case 9:
                name += "Torth";
                break;
            case 10:
                name += "Mon";
                break;
        }

        name += " " + Random.Range(1, 10);

        return name;
    }

}
