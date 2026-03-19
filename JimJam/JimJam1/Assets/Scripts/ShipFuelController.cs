using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipFuelController : MonoBehaviour
{
    public PlayerController Player;
    public Button BuyButton;
    public Text CostText;

    void Start()
    {
        Player = PlayerController.Instance;
    }
	void Update ()
	{
	    var cost = Mathf.FloorToInt((Player.MaxFuel - Player.Fuel) * Player.CurrentSpaceStaion.FuelCostPerUnit);

	    CostText.text = "Cost: $" + cost;
	}

    public void Buy()
    {
        var cost = Mathf.FloorToInt((Player.MaxFuel - Player.Fuel) * Player.CurrentSpaceStaion.FuelCostPerUnit);
        Player.Fuel = Player.MaxFuel;
        Player.Credits -= cost;
        Player.UpdateStats();
        Player.Resupply();

    }
}
