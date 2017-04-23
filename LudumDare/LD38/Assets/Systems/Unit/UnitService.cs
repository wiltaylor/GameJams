using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Assets.Systems.Unit
{
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

        public Unit GetUnitAt(int x, int y)
        {
            return _allUnits.FirstOrDefault(u => u.X == x && u.Y == y);
        }

        public Unit AddUnit(int x, int y, UnitType type, UnitFaction faction)
        {
            var settings = _unitSettings.First(s => s.Type == type);

            var unit = new Unit
            {
                X = x,
                Y = y,
                MaxHp = Random.Range(settings.MinHp, settings.MaxHp),
                Attack = Random.Range(settings.MinAttack, settings.MaxAttack),
                Actions = settings.Actions,
                Faction = faction,
                Type = type
            };

            unit.Hp = unit.MaxHp;
            
            _allUnits.Add(unit);

            UnitChanged(this, new UnitEventArgs{ ChangedUnit = unit});

            return unit;
        }
    }
}
