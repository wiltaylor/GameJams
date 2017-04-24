using Assets.Systems.AI;
using Assets.Systems.CommandManager;
using Assets.Systems.dx;
using Assets.Systems.GameEventManager;
using Assets.Systems.PlayerManager;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;
using UnityEngine.SceneManagement;

public class TurnService
{
    private static TurnService _instance;

    public int Turn { get; private set; }

    public static TurnService Instance
    {
        get { return _instance ?? (_instance = new TurnService()); }    
    }

    public bool EndGame { get; set; }

    public void NextTurn()
    {
        if (DialogueService.Instance.Active)
            return;

        if (EndGame)
        {
            EndGame = false;
            Turn = 0;
            SceneManager.LoadScene("Credits");
        }

        UnitService.Instance.RefreshMovementPoints();
        TileMapService.Instance.Map.TurnRefresh();
        CommandService.Instance.Deselect();
        PlayerService.Instance.IncrementResources();
        AIService.Instance.SpawnAI();
        AIService.Instance.ProcessAI();
        Turn++;

        GameEventService.Instance.CheckForGameEvents();

    }
}
