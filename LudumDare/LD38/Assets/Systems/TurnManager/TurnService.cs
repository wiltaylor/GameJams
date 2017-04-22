using Assets.Systems.TileMap;

public class TurnService
{
    private static TurnService _instance;

    public static TurnService Instance
    {
        get { return _instance ?? (_instance = new TurnService()); }    
    }

    public void NextTurn()
    {
        TileMapService.Instance.Map.DamageMap(1f);
    }
}
