using System.Collections.Generic;
using System.Linq;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;
using UnityEngine;

namespace Assets.Systems.AI
{
    public class AIService
    {
        private static AIService _aiService;
        private AISpawnProfile _aiSpawnProfile;
        private AISpawnSettings _currentSpawnSetting;
        private int _turnsTillSpawn;
        private readonly Queue<UnitType> _spawnQueue = new Queue<UnitType>();

        public static AIService Instance
        {
            get { return _aiService ?? (_aiService = new AIService()); }
        }

        public void AssignAISpawnProfile(AISpawnProfile profile)
        {
            _aiSpawnProfile = profile;
            _currentSpawnSetting = _aiSpawnProfile.Setting.First();
            _turnsTillSpawn = _currentSpawnSetting.WaveFrequency;
        }

        public void ChangeSpawnSetting(string name)
        {
            _currentSpawnSetting = _aiSpawnProfile.Setting.First(s => s.Name == name);
            _turnsTillSpawn = _currentSpawnSetting.WaveFrequency;
        }

        public void ProcessAI()
        {
            var aiunits = UnitService.Instance.GetAllAIUnits();

            foreach (var unit in aiunits)
            {
                var rnd = new System.Random();
                var accessable = UnitService.Instance.GetMovableCords(unit).ToList();

                var playerunit = accessable.FirstOrDefault(a => a.HasUnit &&
                                                        UnitService.Instance.GetUnitAt(a.X, a.Y).Faction ==
                                                        UnitFaction.Player);

                if (playerunit != null)
                {
                    UnitService.Instance.AttackTile(playerunit.X, playerunit.Y, unit, accessable);
                    continue;
                }

                var city = accessable.FirstOrDefault(a => a.IsCity);

                if (city != null)
                {
                    UnitService.Instance.AttackTile(city.X, city.Y, unit, accessable);
                    continue;
                }
                

                var movepoint = accessable.OrderBy(u => rnd.Next()).FirstOrDefault();

                if(movepoint == null)
                    continue;

                UnitService.Instance.MoveUnit(unit, movepoint.X, movepoint.Y);
            }
        }

        public void SpawnAI()
        {
            _turnsTillSpawn--;

            //Prevent spawns from queuing if there is already a backlog.
            if (_spawnQueue.Count > 0 && _turnsTillSpawn <= 0)
                _turnsTillSpawn = 1;

            if (_turnsTillSpawn <= 0)
            {
                _turnsTillSpawn = _currentSpawnSetting.WaveFrequency;

                foreach (var spawntype in _currentSpawnSetting.Spawns)
                {
                    var dice = Random.Range(1, 101);

                    if (spawntype.SpawnChance < dice) continue;

                    for (var q = 0; q < spawntype.Qty; q++)
                    {
                        _spawnQueue.Enqueue(spawntype.Type);
                    }
                }
            }

            if (_spawnQueue.Count <= 0) return;

            var map = TileMapService.Instance.Map;
            var rnd = new System.Random();

            var freeSpawns = new Queue<Building>(map.Buildings.Where(b => b.Type == BuildingType.Hellgate)
                .Where(b => UnitService.Instance.GetUnitAt(b.X, b.Y) == null)
                .OrderBy(b => rnd.Next()));

            while (freeSpawns.Count > 0 && _spawnQueue.Count > 0)
            {
                var spawnPoint = freeSpawns.Dequeue();
                var unittype = _spawnQueue.Dequeue();

                UnitService.Instance.AddUnit(spawnPoint.X, spawnPoint.Y, unittype, UnitFaction.Demon);
            }
        }
    }
}
