using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Systems.CommandManager;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitView : MonoBehaviour
{
    public Text UnitNameText;
    public Text UnitTypeText;
    public Text OwnerText;
    public Text MovementText;
    public Text HpText;
    public Text Combatrating;
    public Button MineButton;
    public Button HealButton;

	private void Update ()
	{
	    if (CommandService.Instance.SelectionState != CommandSelectionState.Unit)
	        return;

	    var unit = CommandService.Instance.SelectedUnit;

	    if (unit == null)
	        return;

	    MovementText.text = "Move: " + unit.MovePointsLeft + "/" + unit.MovePoints;

        MineButton.gameObject.SetActive(CanMine(unit));
        HealButton.gameObject.SetActive(CanHeal(unit));

	    OwnerText.text = unit.Faction == UnitFaction.Player ? "You" : "Deamonic Invasion";

	    HpText.text = "HP: " + Mathf.Round(unit.Hp) + "/" + Mathf.Round(unit.MaxHp);
	    Combatrating.text = "DMG: " + unit.MinAttack + " - " + unit.MaxAttack;

	    UnitNameText.text = unit.Name;

	    switch (unit.Type)
	    {
	        case UnitType.Scout:
	            UnitTypeText.text = "Scout";
                break;
	        case UnitType.Worker:
	            UnitTypeText.text = "Worker";
                break;
	        case UnitType.Deamon:
	            UnitTypeText.text = "Deamon";
	            break;
            case UnitType.Knight:
	            UnitTypeText.text = "Knight";
	            break;
            case UnitType.Spearman:
	            UnitTypeText.text = "Spearman";
	            break;
            case UnitType.Spider:
	            UnitTypeText.text = "Spider";
	            break;
            case UnitType.Wormowl:
	            UnitTypeText.text = "Wormowl";
	            break;
            case UnitType.Clearic:
	            UnitTypeText.text = "Clearic";
	            break;
            default:
	            throw new ArgumentOutOfRangeException();
	    }
	}

    private bool CanHeal(Unit unit)
    {
        return unit.HealAmmount > 0;
    }

    private bool CanMine(Unit unit)
    {
        if(!unit.Actions.Any(a => a == UnitAction.Mine))
            return false;

        var building = TileMapService.Instance.Map.GetBuildingAt(unit.X, unit.Y);

        if (building == null)
            return false;

        if (building.PlayerOwned)
            return false;

        return building.Type == BuildingType.EnergyOre || building.Type == BuildingType.IronOre;
    }

    public void ClickMine()
    {
        var unit = CommandService.Instance.SelectedUnit;
        TileMapService.Instance.Map.MineBuilding(unit.X, unit.Y);
    }

    public void ClickHeal()
    {
        var unit = CommandService.Instance.SelectedUnit;
        UnitService.Instance.CastHeal(unit);
    }
}
