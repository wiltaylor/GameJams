using UnityEditor;

namespace Assets.Systems.Unit
{
    public enum UnitFaction
    {
        Player,
        Demon
    }

    public class Unit
    {
        public int X { get; set; }
        public int Y { get; set; }
        public float MaxHp { get; set; }
        public float Hp { get; set; }
        public float MaxAttack { get; set; }
        public float MinAttack { get; set; }
        public int MovePoints { get; set; }
        public int MovePointsLeft { get; set; }
        public UnitAction[] Actions { get; set; }
        public UnitFaction Faction { get; set; }
        public UnitType Type { get; set; }
        public GUID UnitId { get; set; } 
        public int ViewRange { get; set; }
    }
}
