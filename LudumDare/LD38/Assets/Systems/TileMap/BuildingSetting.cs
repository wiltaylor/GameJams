using Assets.Systems.Unit;
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
        public int FaithPerTurn;
        public int IronPerOwn;
        public int HumanPerOwn;
        public float MaxHp;
        public float MinHp;
        public float MinDamage;
        public float MaxDamage;
        public UnitAction CaptureAction;
        public bool AiTarget;
    }
}
