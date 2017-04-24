using System.Linq;
using Assets.Systems.CommandManager;
using Assets.Systems.Music;
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

    public Text IronText;
    public Text FaithText;
    public Text HumanText;

    public void Start()
    {
        _button = GetComponent<Button>();
    }

    private void FixedUpdate()
    {
        
        if (_settings == null)
            _settings = UnitService.Instance.GetUnitSettings(Type);

        RenderText();

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

    private void RenderText()
    {
        IronText.text = _settings.IronCost.ToString();
        FaithText.text = _settings.FaithCost.ToString();
        HumanText.text = _settings.HumanCost.ToString();

        IronText.color = _settings.IronCost > PlayerService.Instance.Iron - PlayerService.Instance.IronUsed ? Color.red : Color.white;
        FaithText.color = _settings.FaithCost > PlayerService.Instance.Faith ? Color.red : Color.white;
        HumanText.color = _settings.HumanCost > PlayerService.Instance.TotalHumans - PlayerService.Instance.UsedHumans ? Color.red : Color.white;
    }

    public void Click()
    {
        var building = CommandService.Instance.SelectedBuilding;

        UnitService.Instance.AddUnit(building.X, building.Y, Type, UnitFaction.Player);

        building.HasBuiltThisTurn = true;
        
        MusicService.Instance.PlaySfx(SfxType.BuildUnit);
        
    }
}
