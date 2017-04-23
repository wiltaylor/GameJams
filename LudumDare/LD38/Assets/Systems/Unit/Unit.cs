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
        public float Attack { get; set; }
        public float MovePoints { get; set; }
        public UnitAction[] Actions { get; set; }
        public UnitFaction Faction { get; set; }
        public UnitType Type { get; set; }
    }
}
