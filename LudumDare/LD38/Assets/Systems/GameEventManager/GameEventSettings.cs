using System;
using Assets.Systems.TileMap;

namespace Assets.Systems.GameEventManager
{
    public enum GameEventTriggerType
    {
        Turn,
        BuildingsControlledOrMore,
        BuildingsControlledOrLess,
        BuildingsExist
    }

    public enum GameEventTriggerActionType
    {
        SwitchSpawnProfile,
        EndGame,
        Dialogue,
        ChangePlaylist
    }

    [Serializable]
    public class GameEventTrigger
    {
        public GameEventTriggerType TriggerType;
        public int TriggerValue;
        public BuildingType BuildingType;
        public GameEventTriggerActionType ActionType;
        public string ActionValue;
    }
}
