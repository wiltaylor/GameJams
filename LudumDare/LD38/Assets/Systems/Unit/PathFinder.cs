using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.View.TileMap;
using Assets.Systems.TileMap;

namespace Assets.Systems.Unit
{
    public class PathFinder
    {
        public static IEnumerable<TileCords> FindMoveableLocations(int x, int y, int points)
        {
            return CheckTile(x,y,points, true);
        }

        private static IEnumerable<TileCords> CheckTile(int x, int y, int points, bool freeCost = false)
        {
            var map = TileMapService.Instance.Map;

            if (x < 0)
                x += map.MapWidth;
            if (y < 0)
                y += map.MapHeight;
            if (x >= map.MapWidth)
                x -= map.MapWidth;
            if (y >= map.MapHeight)
                y -= map.MapHeight;

            var avilpoints = new List<TileCords>();
            var tile = map.MapData[x, y];
            var building = map.GetBuildingAt(x, y);
            var unit = UnitService.Instance.GetUnitAt(x, y);

            if (!tile.Passable && !freeCost)
                return avilpoints;

            points -= freeCost ?  0 : tile.MoveCost;
            avilpoints.Add(new TileCords{X = x, Y = y, Cost = tile.MoveCost, HasBuilding = building != null, HasUnit = unit != null});

            if (points <= 0 && !freeCost)
                return avilpoints;

            if(!avilpoints.Any(a => a.X == TileMapUtil.CalculateX(x - 1) && a.Y == y))
                avilpoints.AddRange(CheckTile(x + 1, y, points));
            if (!avilpoints.Any(a => a.X == TileMapUtil.CalculateX(x - 1) && a.Y == y))
                avilpoints.AddRange(CheckTile(x - 1, y, points));
            if (!avilpoints.Any(a => a.X == x && a.Y == TileMapUtil.CalculateY(y - 1)))
                avilpoints.AddRange(CheckTile(x, y - 1, points));
            if (!avilpoints.Any(a => a.X == x && a.Y == TileMapUtil.CalculateY(y + 1)))
                avilpoints.AddRange(CheckTile(x, y + 1, points));


            return avilpoints;
        }
    }
}
