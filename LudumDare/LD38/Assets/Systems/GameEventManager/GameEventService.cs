using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Systems.AI;
using Assets.Systems.dx;
using Assets.Systems.Music;
using Assets.Systems.TileMap;

namespace Assets.Systems.GameEventManager
{
    public class GameEventService
    {
        private static GameEventService _instance;
        private readonly List<GameEventTrigger> _eventTriggers = new List<GameEventTrigger>();

        public static GameEventService Instance
        {
            get { return _instance ?? (_instance = new GameEventService()); }
        }

        public void AssignEventTriggers(GameEventTrigger[] triggers)
        {
            _eventTriggers.AddRange(triggers);
        }
        

        public void CheckForGameEvents()
        {
            var spent = new List<GameEventTrigger>();

            foreach (var trig in _eventTriggers)
            {
                if (TurnService.Instance.EndGame)
                    break;

                switch (trig.TriggerType)
                {
                    case GameEventTriggerType.Turn:
                    {
                        if (TurnService.Instance.Turn == trig.TriggerValue)
                        {
                            RunTrigger(trig);
                            spent.Add(trig);
                        }
                        break;
                    }
                    case GameEventTriggerType.BuildingsControlledOrMore:
                    {
                        var t = trig; //Closure compatability with unity.
                        if (TileMapService.Instance.Map.Buildings.Count(
                                b => b.PlayerOwned && b.Type == t.BuildingType) >= trig.TriggerValue)
                        {
                            RunTrigger(trig);
                            spent.Add(trig);
                        }
                        break;
                    }
                    case GameEventTriggerType.BuildingsControlledOrLess:
                    {
                        var t = trig; //Closure compatability with unity.
                        if (TileMapService.Instance.Map.Buildings.Count(
                                b => b.PlayerOwned && b.Type == t.BuildingType) <= trig.TriggerValue)
                        {
                            RunTrigger(trig);
                            spent.Add(trig);
                        }
                        break;
                    }
                    case GameEventTriggerType.BuildingsExist:
                    { 
                        var t = trig; //Closure compatability with unity.
                        if (TileMapService.Instance.Map.Buildings.Count(
                                b => b.Type == t.BuildingType) >= trig.TriggerValue)
                        {
                            RunTrigger(trig);
                            spent.Add(trig);
                        }
                        break;
                    }
                        
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var trig in spent)
                _eventTriggers.Remove(trig);
        }

        private void RunTrigger(GameEventTrigger trig)
        {
            switch (trig.ActionType)
            {
                case GameEventTriggerActionType.SwitchSpawnProfile:
                    AIService.Instance.ChangeSpawnSetting(trig.ActionValue);
                    break;
                case GameEventTriggerActionType.EndGame:
                    DialogueService.Instance.StartDialoue(trig.ActionValue);
                    TurnService.Instance.EndGame = true;
                    break;
                case GameEventTriggerActionType.Dialogue:
                    DialogueService.Instance.StartDialoue(trig.ActionValue);
                    break;
                case GameEventTriggerActionType.ChangePlaylist:
                    MusicService.Instance.SetPlaylist(trig.ActionValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        
    }
}
