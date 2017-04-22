using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Systems.TileMap
{
    [Serializable]
    public class TileMaplGeneratorIsland
    {
        public TileType Tile;
        public int MaxWidth;
        public int MinWidth;
        public int MaxHeight;
        public int MinHeight;
    }
}
