using System;
using Assets.Systems.Unit;

namespace Assets.Systems.AI
{
    [Serializable]
    public class AISpawnSettings
    {
        public string Name;
        public AISpawnChance[] Spawns;
        public int WaveFrequency;
    }

    [Serializable]
    public class AISpawnChance
    {
        public UnitType Type;
        public int SpawnChance;
        public int Qty;
    }
}
