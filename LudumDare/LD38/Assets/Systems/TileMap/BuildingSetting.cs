using UnityEngine;

namespace Assets.Systems.TileMap
{
    [CreateAssetMenu(fileName = "Building", menuName = "MapGenerator/BuildingSettings", order = 1)]
    public class BuildingSetting : ScriptableObject
    {
        public BuildingType Type;
        public TileType[] AllowedToBuildOn;
        public int BuildingDistance;
        public bool Mineable;
        public float MaxHpModifier;
        public float MinHpModifier;
        public int HpModifierRange;
    }
}
