using System;
using System.Linq;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;

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
        public Unit.Unit SelectedUnit { get; private set; }
        public TileCords[] UnitMoveRange { get; private set; }

        public event EventHandler HighlightChanged = (sender, args) => { };
        
        public static CommandService Instance
        {
            get { return _instance ?? (_instance = new CommandService()); }
        }

        private CommandService()
        {
            SelectionState = CommandSelectionState.Nothing;
        }

        public void ReportTileClick(int x, int y, int btn)
        {
            if(btn == 0)
                HandleLeftClick(x,y);

            if (btn == 1)
                HandleRightClick(x, y);
        }

        private void HandleRightClick(int x, int y)
        {
            if (SelectionState != CommandSelectionState.Unit || SelectedUnit.Faction != UnitFaction.Player) return;

            var coords = UnitMoveRange.FirstOrDefault(m => m.X == x && m.Y == y);

            if (coords == null) return;

            UnitService.Instance.MoveUnit(SelectedUnit, x, y);
            SelectionState = CommandSelectionState.Nothing;
            RefreshSelection();
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
            var unit = UnitService.Instance.GetUnitAt(x, y);

            switch (_clickCount)
            {
                default:
                    _clickCount = 0;
                    SelectionState = CommandSelectionState.Nothing;
                    break;
                case 1:
                    if (unit != null)
                    {
                        SelectedUnit = unit;
                        SelectionState = CommandSelectionState.Unit;
                        UnitMoveRange = UnitService.Instance.GetMovableCords(unit).ToArray();
                        break;
                    }

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

            RefreshSelection();
        }

        private void RefreshSelection()
        {
            switch (SelectionState)
            {
                case CommandSelectionState.Nothing:
                    SelectedBuilding = null;
                    SelectedUnit = null;
                    UnitMoveRange = null;
                    break;
                case CommandSelectionState.Building:
                    SelectedUnit = null;
                    UnitMoveRange = null;
                    break;
                case CommandSelectionState.Unit:
                    SelectedBuilding = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            HighlightChanged(this, EventArgs.Empty);
        }
    }
}
