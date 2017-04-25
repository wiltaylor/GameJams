using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.View.TileMap;
using Assets.Systems.Music;
using Assets.Systems.NameGenerator;
using Assets.Systems.PlayerManager;
using Assets.Systems.TileMap;
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

        public Unit GetUnitById(Guid id)
        {
            return _allUnits.FirstOrDefault(u => u.UnitId == id);
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

            if (unit.Faction == UnitFaction.Player)
            {
                var settings = _unitSettings.First(s => s.Type == unit.Type);
                PlayerService.Instance.IronUsed -= settings.IronCost;
                PlayerService.Instance.UsedHumans -= settings.HumanCost;
            }

            UnitChanged(this, new UnitEventArgs{ ChangedUnit = unit});
        }

        public void CastHeal(Unit unit)
        {
            var map = TileMapService.Instance.Map;
            var top = unit.Y - unit.Healrange;
            var left = unit.X - unit.Healrange;
            var bottom = unit.Y + unit.Healrange;
            var right = unit.X + unit.Healrange;

            for (var x = left; x <= right; x++)
            {
                for (var y = top; y <= bottom; y++)
                {
                    var foundunit = GetUnitAt(TileMapUtil.CalculateX(x), TileMapUtil.CalculateY(y));

                    if (foundunit == null || foundunit.Faction != UnitFaction.Player || foundunit.UnitId == unit.UnitId) continue;

                    foundunit.Hp += unit.HealAmmount;

                    if (foundunit.Hp > foundunit.MaxHp)
                        foundunit.Hp = foundunit.MaxHp;
                }
            }

            unit.Hp -= unit.HealDamage;
            UnitChanged(this, new UnitEventArgs{ ChangedUnit = unit});
        }

        public Unit AddUnit(int x, int y, UnitType type, UnitFaction faction, bool noResourceCost = false)
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
                UnitId = Guid.NewGuid(),
                ViewRange = settings.ViewRange,
                Name = NameService.Instance.NewUnitName(faction, type),
                Healrange =  settings.HealRange,
                HealAmmount = settings.HealAmmount,
                HealDamage = settings.HealDamage,
                MineDamage = settings.MineDamage
                
            };

            unit.Hp = unit.MaxHp;
            unit.MovePointsLeft = unit.MovePoints;
            
            _allUnits.Add(unit);

            if (unit.Faction == UnitFaction.Player)
            {
                TileMapService.Instance.Map.RevealMap(x, y, unit.ViewRange);

                if (!noResourceCost)
                {
                    PlayerService.Instance.Faith -= settings.FaithCost;
                    PlayerService.Instance.IronUsed += settings.IronCost;
                    PlayerService.Instance.UsedHumans += settings.HumanCost;
                }
            }
                
            UnitChanged(this, new UnitEventArgs{ ChangedUnit = unit});

            return unit;
        }

        public void MoveUnit(Unit unit, int x, int y)
        {
            unit.X = x;
            unit.Y = y;
            UnitChanged(this, new UnitEventArgs {ChangedUnit = unit});

            if(unit.Faction == UnitFaction.Player)
                TileMapService.Instance.Map.RevealMap(x, y, unit.ViewRange);
        }

        public void RefreshMovementPoints()
        {
            foreach (var unit in _allUnits)
            {
                unit.MovePointsLeft = unit.MovePoints;
                if(unit.Hp <= 0f)
                    KillUnitAt(unit.X, unit.Y);
            }
        }

        public void AttackTile(int x, int y, Unit unit, IEnumerable<TileCords> attackRange)
        {
            //Check attack type.
            var range = attackRange.ToList();
            var targetunit = GetUnitAt(x, y);

            if (targetunit != null && targetunit.Faction == unit.Faction)
                return;

            MusicService.Instance.PlaySfx(unit.Faction == UnitFaction.Player ? SfxType.Attack : SfxType.DemonAttack);
            
            if (targetunit != null)
            {
                AttackUnit(targetunit, x, y, unit, range);
                return;
            }

            var building = TileMapService.Instance.Map.GetBuildingAt(x, y);

            if (building == null)
                return;

            if (building.PlayerOwned && unit.Faction == UnitFaction.Player)
                return;

            AttackBuilding(building, x, y, unit, range);
        }

        private void AttackUnit(Unit targetUnit, int x, int y, Unit unit, IList<TileCords> attackRange)
        {
            if (!IsAttackPosition(x, y, unit.X, unit.Y))
            {
                var attackpos = attackRange.FirstOrDefault(c => IsAttackPosition(x, y, c.X, c.Y));
                if (attackpos == null)
                    return;

                MoveUnit(unit, attackpos.X, attackpos.Y);
            }

            unit.MovePointsLeft = 0;
            targetUnit.Hp -= Random.Range(unit.MinAttack, unit.MaxAttack);
            unit.Hp -= Random.Range(targetUnit.MinAttack, targetUnit.MaxAttack);

            if (unit.Hp <= 0)
            {
                KillUnitAt(unit.X, unit.Y);
                if (targetUnit.Hp < 0)
                    targetUnit.Hp = 0.5f;
                return;
            }

            if (targetUnit.Hp > 0)
                return;

            KillUnitAt(x, y);

            var building = TileMapService.Instance.Map.GetBuildingAt(x, y);

            if(building == null)
                MoveUnit(unit, x, y);
        }

        private void AttackBuilding(Building building, int x, int y, Unit unit, IEnumerable<TileCords> attackRange)
        {
            if (unit.Faction == UnitFaction.Player && (building.Type != BuildingType.City && building.Type != BuildingType.Hellgate))
                return;

            if (!IsAttackPosition(x, y, unit.X, unit.Y))
            {
                var newAttackPosition =
                    attackRange.FirstOrDefault(c => IsAttackPosition(x, y, c.X, c.Y) && c.HasBuilding == false && c.HasUnit == false);

                if (newAttackPosition == null)
                    return;

                MoveUnit(unit, newAttackPosition.X, newAttackPosition.Y);
            }

            unit.MovePointsLeft = 0;
            building.Hp -= Random.Range(unit.MinAttack, unit.MaxAttack);
            unit.Hp -= Random.Range(building.MinDamage, building.MaxDamage);

            if (unit.Hp <= 0)
            {
                KillUnitAt(unit.X, unit.Y);
                if (building.Hp < 0)
                    building.Hp = 1;
                return;
            }

            if (!(building.Hp <= 0)) return;

            if (unit.Faction == UnitFaction.Player && building.Type == BuildingType.City)
            {
                building.PlayerOwned = true;
                building.Hp = building.MaxHp;
                MoveUnit(unit, x, y);
                TileMapService.Instance.Map.RefreshBuilding(building);
            }
            else
            {
                MoveUnit(unit, x, y);
                TileMapService.Instance.Map.DamageTile(x,y,100000);
                TileMapService.Instance.Map.RefreshBuilding(building);
            }
        }

        public IEnumerable<Unit> GetAllAIUnits()
        {
            return _allUnits.Where(u => u.Faction == UnitFaction.Demon);
        }

        private static bool IsAttackPosition(int x, int y, int targetX, int targetY)
        {
            var inAttackPosition =
                targetX == x && TileMapUtil.CalculateY(targetY - 1) == y ||
                targetX == x && TileMapUtil.CalculateY(targetY + 1) == y ||
                TileMapUtil.CalculateX(targetX - 1) == x && targetY == y ||
                TileMapUtil.CalculateX(targetX + 1) == x && targetY == y;
            return inAttackPosition;
        }
    }
}
