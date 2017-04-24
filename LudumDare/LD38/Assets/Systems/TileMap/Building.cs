using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Systems.TileMap
{
    public class Building
    {
        public string Name { get; set; }
        public BuildingType Type { get; set; }
        public bool PlayerOwned { get; set; }
        public int Population { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool HasBuiltThisTurn { get; set; }
        public int FaithPerTurn { get; set; }
        public int IronPerOwn { get; set; }
        public int HumanPerOwn { get; set; }
        public float Hp { get; set; }
        public float MaxHp { get; set; }
        public float MinDamage { get; set; }
        public float MaxDamage { get; set; }
    }
}
