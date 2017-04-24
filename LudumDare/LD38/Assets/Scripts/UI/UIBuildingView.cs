using System;
using Assets.Systems.CommandManager;
using Assets.Systems.TileMap;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingView : MonoBehaviour
{
    public Text BuildingName;
    public Text BuildingType;
    public Text BuildingHp;
    public Text IronText;
    public Text FaithText;
    public Text HumanText;
    public Text ComabatText;

    public GameObject[] BuildButtons;
	
	void Update ()
	{
	    var building = CommandService.Instance.SelectedBuilding;

	    if (building == null)
	        return;

	    if (building.PlayerOwned && !building.HasBuiltThisTurn && building.Type == Assets.Systems.TileMap.BuildingType.City)
	    {
	        foreach (var b in BuildButtons)
	        {
                b.SetActive(true);
	        }
	    }
	    else
	    {
	        foreach (var b in BuildButtons)
	            b.SetActive(false);
        }

	    RenderResources(building);

	    ComabatText.text = "Defence Rating:" + building.MinDamage + "-" + building.MinDamage;

        if (building.MaxHp > 0)
	        BuildingHp.text = "HP:" + Mathf.Round(building.Hp) + "/" + Mathf.Round(building.MaxHp);
        
	    switch (building.Type)
	    {
	        case Assets.Systems.TileMap.BuildingType.City:
	            BuildingName.text = building.Name;
	            BuildingType.text = "City";
	            break;
	        case Assets.Systems.TileMap.BuildingType.IronOre:
	            BuildingName.text = "";
                BuildingType.text = "Iron Ore Deposit";
                break;
	        case Assets.Systems.TileMap.BuildingType.EnergyOre:
	            BuildingName.text = "";
                BuildingType.text = "Energy Temple";
                break;
	        default:
	            throw new ArgumentOutOfRangeException();
	    }

	}

    private void RenderResources(Building building)
    {
        if (building.FaithPerTurn > 0)
        {
            FaithText.text = "+" + building.FaithPerTurn + " per turn";
            FaithText.color = Color.green;
        }
        else if (building.FaithPerTurn < 0)
        {
            FaithText.text = "-" + building.FaithPerTurn + " per turn";
            FaithText.color = Color.red;
        }
        else
        {
            FaithText.text = "None";
            FaithText.color = Color.white;
        }

        if (building.IronPerOwn > 0)
        {
            IronText.text = "+" + building.IronPerOwn;
            IronText.color = Color.green;
        }
        else if (building.IronPerOwn < 0)
        {
            IronText.text = "-" + building.IronPerOwn;
            IronText.color = Color.red;
        }
        else
        {
            IronText.text = "None";
            IronText.color = Color.white;
        }

        if (building.HumanPerOwn > 0)
        {
            HumanText.text = "+" + building.HumanPerOwn;
            HumanText.color = Color.green;
        }
        else if (building.HumanPerOwn < 0)
        {
            HumanText.text = "-" + building.HumanPerOwn;
            HumanText.color = Color.red;
        }
        else
        {
            HumanText.text = "None";
            HumanText.color = Color.white;
        }
    }
}
