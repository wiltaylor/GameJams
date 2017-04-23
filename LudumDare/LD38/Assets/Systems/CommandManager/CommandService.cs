using System;
using Assets.Systems.TileMap;
using UnityEngine;

namespace Assets.Systems.CommandManager
{
    public enum CommandSelectionState
    {
        Nothing,
        Building,
        Unit
    }

    public class CommandService
    {
        private static CommandService _instance;
        private int _lastX = -1;
        private int _lastY = -1;
        private int _clickCount = 0;

        public CommandSelectionState SelectionState { get; private set; }
        public Building SelectedBuilding { get; private set; }
        
        public static CommandService Instance
        {
            get { return _instance ?? (_instance = new CommandService()); }
        }

        public void ReportTileClick(int x, int y, int btn)
        {
            if(btn == 0)
                HandleLeftClick(x,y);
        }

        private void HandleLeftClick(int x, int y)
        {
            if (_lastX == x && _lastY == y)
            {
                _clickCount++;
            }
            else
            {
                _lastY = y;
                _lastX = x;
                _clickCount = 1;
            }

            var map = TileMapService.Instance.Map;
            var building = map.GetBuildingAt(x, y);
            //Unit

            switch (_clickCount)
            {
                default:
                    _clickCount = 0;
                    SelectionState = CommandSelectionState.Nothing;
                    break;
                case 1:
                    //first check unit

                    if (building != null)
                    {
                        SelectedBuilding = building;
                        SelectionState = CommandSelectionState.Building;
                        break;
                    }
                    _clickCount = 0;
                    SelectionState = CommandSelectionState.Nothing;
                    break;
                case 2:
                    if (building != null && SelectedBuilding == null)
                    {
                        SelectionState = CommandSelectionState.Building;
                        SelectedBuilding = building;
                        break;
                    }
                    _clickCount = 0;
                    SelectionState = CommandSelectionState.Nothing;
                    break;
            }

            switch (SelectionState)
            {
                case CommandSelectionState.Nothing:
                    SelectedBuilding = null;
                    break;
                case CommandSelectionState.Building:
                    break;
                case CommandSelectionState.Unit:
                    SelectedBuilding = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
