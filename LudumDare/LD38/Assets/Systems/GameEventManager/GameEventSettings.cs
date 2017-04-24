using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Systems.TileMap;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Systems.GameEventManager
{
    public enum GameEventTriggerType
    {
        Turn,
        BuildingsControlled,
        BuildingsExist
    }

    public enum GameEventTriggerActionType
    {
        SwitchSpawnProfile,
        EndGame,
        Dialogue
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
