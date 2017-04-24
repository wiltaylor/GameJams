using UnityEngine;

namespace Assets.Systems.TileMap
{
    [CreateAssetMenu(fileName = "MapGeneratorSettings", menuName = "MapGenerator/TileData", order = 1)]
    public class TileSettings : ScriptableObject
    {
        public TileType TileId;
        public float MaxStartHp;
        public float MinStartHp;
        public float DecayRate;
        public int MoveCost;
        public bool Passable;
    }
}
