using UnityEngine;

namespace Assets.Systems.TileMap
{
    [CreateAssetMenu(fileName = "MapGeneratorSettings", menuName = "MapGenerator/MapGeneratorSettings", order = 1)]
    public class TileMapGeneratorSettings : ScriptableObject
    {
        public int Width;
        public int Height;
        public string Name;
        public TileType FillType;
        public int MutateCycles;
        public TileMaplGeneratorIsland[] Islands;
        public BuildingGeneratorSetting[] Buildings;
        public int TileDamageRange;
        public float TileDamageAmmount;
        public float DamagePerTurn = 1f;
    }
}
