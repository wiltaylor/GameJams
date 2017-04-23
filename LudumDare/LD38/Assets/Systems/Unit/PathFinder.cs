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
            return points <= 0 ? new List<TileCords>() : CheckTile(x,y,points, true);
        }

        private static IEnumerable<TileCords> CheckTile(int x, int y, int points, bool freeCost = false)
        {
            if(points <= 0)
                return new List<TileCords>();

            var map = TileMapService.Instance.Map;
            var avilpoints = new List<TileCords>();
            var tile = map.MapData[x, y];
            var building = map.GetBuildingAt(x, y);
            var unit = UnitService.Instance.GetUnitAt(x, y);

            if (!tile.Passable && !freeCost)
                return avilpoints;

            points -= freeCost ?  0 : tile.MoveCost;
            avilpoints.Add(new TileCords{X = x, Y = y, Cost = tile.MoveCost, HasBuilding = building != null, HasUnit = unit != null});

            if(!avilpoints.Any(a => a.X == TileMapUtil.CalculateX(x + 1) && a.Y == y))
                avilpoints.AddRange(CheckTile(TileMapUtil.CalculateX(x + 1), y, points));
            if (!avilpoints.Any(a => a.X == TileMapUtil.CalculateX(x - 1) && a.Y == y))
                avilpoints.AddRange(CheckTile(TileMapUtil.CalculateX(x - 1), y, points));
            if (!avilpoints.Any(a => a.X == x && a.Y == TileMapUtil.CalculateY(y - 1)))
                avilpoints.AddRange(CheckTile(x, TileMapUtil.CalculateY(y - 1), points));
            if (!avilpoints.Any(a => a.X == x && a.Y == TileMapUtil.CalculateY(y + 1)))
                avilpoints.AddRange(CheckTile(x, TileMapUtil.CalculateY(y + 1), points));
            
            return avilpoints;
        }
    }
}
