using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets.Systems.TileMap;

namespace Assets.Scripts.View.TileMap
{
    public class TileMapUtil
    {
        public static int CalculateX(int x)
        {
            var map = TileMapService.Instance.Map;

            if (x < 0)
                return x + map.MapWidth;

            if (x >= map.MapWidth)
                return x - map.MapWidth;

            return x;
        }

        public static int CalculateY(int y)
        {
            var map = TileMapService.Instance.Map;

            if (y < 0)
                return y + map.MapHeight;

            if (y >= map.MapHeight)
                return y - map.MapHeight;

            return y;
        }
    }
}
