using Assets.Systems.TileMap;
using UnityEngine;

namespace Assets.Systems.Unit
{
    [CreateAssetMenu(fileName = "UnitSettings", menuName = "Unit/Unit Settings", order = 1)]
    public class UnitSettings : ScriptableObject
    {
        public UnitType Type;
        public string Name;
        public float MaxHp;
        public float MinHp;
        public float MaxAttack;
        public float MinAttack;
        public UnitAction[] Actions;
        public bool CanSpawnInCity;
        public bool CanSpawnInDemonGate;
        public TileType[] CanSpawnOn;
        public int MovePoints;
        public int FaithCost;
        public int HumanCost;
        public int IronCost;
        public int ViewRange;
    }
}
