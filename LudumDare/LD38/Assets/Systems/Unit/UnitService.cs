using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Systems.TileMap;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Assets.Systems.Unit
{
    public class TileCords
    {
        public int X;
        public int Y;
        public int Cost;
        public bool HasUnit;
        public bool HasBuilding;
    }

    public class UnitService
    {
        private static UnitService _instance;

        private UnitSettings[] _unitSettings;
        private readonly List<Unit> _allUnits = new List<Unit>();

        public EventHandler<UnitEventArgs> UnitChanged = (sender, args) => {};

        public static UnitService Instance
        {
            get { return _instance ?? (_instance = new UnitService()); }
        }

        public void AssignUnitDefinition(UnitSettings[] settings)
        {
            _unitSettings = settings;
        }

        public UnitSettings GetUnitSettings(UnitType type)
        {
            return _unitSettings.FirstOrDefault(u => u.Type == type);
        }

        public IEnumerable<TileCords> GetMovableCords(Unit unit)
        {
            return PathFinder.FindMoveableLocations(unit.X, unit.Y, unit.MovePointsLeft);
        }

        public Unit GetUnitAt(int x, int y)
        {
            return _allUnits.FirstOrDefault(u => u.X == x && u.Y == y);
        }

        public void KillUnitAt(int x, int y)
        {
            var unit = GetUnitAt(x, y);

            if (unit == null)
                return;

            _allUnits.Remove(unit);

            unit.Hp = 0;
        }

        public Unit AddUnit(int x, int y, UnitType type, UnitFaction faction)
        {
            var settings = _unitSettings.First(s => s.Type == type);

            var unit = new Unit
            {
                X = x,
                Y = y,
                MaxHp = Random.Range(settings.MinHp, settings.MaxHp),
                MaxAttack = settings.MaxAttack,
                MinAttack =  settings.MinAttack,
                Actions = settings.Actions,
                Faction = faction,
                Type = type,
                MovePoints = settings.MovePoints,
                UnitId = GUID.Generate(),
                ViewRange = settings.ViewRange
            };

            unit.Hp = unit.MaxHp;
            unit.MovePointsLeft = unit.MovePoints;
            
            _allUnits.Add(unit);

            TileMapService.Instance.Map.RevealMap(x, y, unit.ViewRange);
            UnitChanged(this, new UnitEventArgs{ ChangedUnit = unit});

            return unit;
        }

        public void MoveUnit(Unit unit, int x, int y)
        {
            unit.X = x;
            unit.Y = y;
            UnitChanged(this, new UnitEventArgs {ChangedUnit = unit});
            TileMapService.Instance.Map.RevealMap(x, y, unit.ViewRange);
        }

        public void RefreshMovementPoints()
        {
            foreach (var unit in _allUnits)
            {
                unit.MovePointsLeft = unit.MovePoints;
            }
        }
    }
}
