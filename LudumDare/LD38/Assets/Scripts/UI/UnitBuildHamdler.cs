using Assets.Systems.CommandManager;
using Assets.Systems.Unit;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuildHamdler : MonoBehaviour
{
    public UnitType Type;

    private Button _button;

    public void Start()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        var building = CommandService.Instance.SelectedBuilding;

        if (building == null)
            return;

        var unit = UnitService.Instance.GetUnitAt(building.X, building.Y);

        _button.enabled = unit == null;
    }

    public void Click()
    {
        var building = CommandService.Instance.SelectedBuilding;

        UnitService.Instance.AddUnit(building.X, building.Y, Type, UnitFaction.Player);

        building.HasBuiltThisTurn = true;
    }
}
