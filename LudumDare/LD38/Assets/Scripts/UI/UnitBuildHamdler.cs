using System.Linq;
using Assets.Systems.CommandManager;
using Assets.Systems.PlayerManager;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuildHamdler : MonoBehaviour
{
    public UnitType Type;

    private Button _button;
    private UnitSettings _settings;

    public void Start()
    {
        _button = GetComponent<Button>();
    }

    private void FixedUpdate()
    {
        if(_settings == null)
            _settings = UnitService.Instance.GetUnitSettings(Type);

        var building = CommandService.Instance.SelectedBuilding;

        if (building == null)
            return;

        if (_settings.IronCost > PlayerService.Instance.Iron || _settings.FaithCost > PlayerService.Instance.Faith ||
            _settings.HumanCost > PlayerService.Instance.TotalHumans - PlayerService.Instance.UsedHumans)
        {
            _button.enabled = false;
            return;
        }

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
