using UnityEngine;

namespace Assets.Systems.TileMap
{
    public enum TileType
    {
        Void = 0,
        Water = 1,
        Plains = 2,
        Desert = 3,
        Forest = 4,
        Arctic = 5,
        Wasteland = 6,
        Mountain = 7
    }

    [CreateAssetMenu(fileName = "MapGeneratorSettings", menuName = "MapGenerator/MapGeneratorSettings", order = 1)]
    public class TileMapGeneratorSettings : ScriptableObject
    {
        public int Width;
        public int Height;
        public string Name;
        public TileType FillType;
        public int MutateCycles;
        public TileMaplGeneratorIsland[] Islands;
        public int TileDamageRange;
        public float TileDamageAmmount;
    }
}
