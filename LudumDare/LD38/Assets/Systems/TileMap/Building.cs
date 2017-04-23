using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Systems.TileMap
{
    public class Building
    {
        public BuildingType Type { get; set; }
        public bool PlayerOwned { get; set; }
        public int Population { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool HasBuiltThisTurn { get; set; }
        public int FaithPerTurn { get; set; }
        public int IronPerOwn { get; set; }
        public int HumanPerOwn { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int DefenceRating { get; set; }

    }
}
