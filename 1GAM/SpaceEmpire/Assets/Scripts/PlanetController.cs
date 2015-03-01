using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlanetController : MonoBehaviour
{
    public static GameObject LastPlanet;

    public Sprite EnamyOwnedPlanet;
    public Sprite PlayerOwnedPlanet;
    public bool PlayerOwned;
    public int TroopProductionRate = 1;
    public int FoodProductionRate = 1;
    public int ShipProductionRate = 1;
    public int FuelProductionRate = 1;
    public bool SpySats;
    public string PlanetName = "Planet Name Here";
    public int Troops = 100;
    public int Ships = 100;
    public int Food = 100;
    public int Fuel = 100;
    private Image _planetImage;

    public void Awake()
    {
        _planetImage = GetComponent<Image>();
    }

    public void UpdateOwner()
    {
        _planetImage.sprite = PlayerOwned ? PlayerOwnedPlanet : EnamyOwnedPlanet;
    }


    public void OnClick()
    {
        GameManager.Instance.Selector.SelectPlanet(gameObject);
    }
}
