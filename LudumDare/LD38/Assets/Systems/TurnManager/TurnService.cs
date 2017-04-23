using Assets.Systems.AI;
using Assets.Systems.CommandManager;
using Assets.Systems.PlayerManager;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;

public class TurnService
{
    private static TurnService _instance;

    public int Turn { get; private set; }

    public static TurnService Instance
    {
        get { return _instance ?? (_instance = new TurnService()); }    
    }

    public void NextTurn()
    {
        UnitService.Instance.RefreshMovementPoints();
        TileMapService.Instance.Map.TurnRefresh();
        CommandService.Instance.Deselect();
        PlayerService.Instance.IncrementResources();
        AIService.Instance.SpawnAI();
        AIService.Instance.ProcessAI();
        Turn++;
    }
}
