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

    }
}
