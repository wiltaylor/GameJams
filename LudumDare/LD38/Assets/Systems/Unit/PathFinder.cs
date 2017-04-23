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
            return points <= 0 ? new List<TileCords>() : CheckTile(x,y,points, 0,true);
        }

        private static IEnumerable<TileCords> CheckTile(int x, int y, int points, int cost, bool freeCost = false)
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

            if (!freeCost)
                cost += tile.MoveCost;

            points -= freeCost ?  0 : tile.MoveCost;
            avilpoints.Add(new TileCords{X = x, Y = y, Cost = cost, HasBuilding = building != null, HasUnit = unit != null, IsCity = building != null && building.Type == BuildingType.City, IsHellGate = building != null && building.Type == BuildingType.Hellgate });

            if(!avilpoints.Any(a => a.X == TileMapUtil.CalculateX(x + 1) && a.Y == y))
                avilpoints.AddRange(CheckTile(TileMapUtil.CalculateX(x + 1), y, points, cost));
            if (!avilpoints.Any(a => a.X == TileMapUtil.CalculateX(x - 1) && a.Y == y))
                avilpoints.AddRange(CheckTile(TileMapUtil.CalculateX(x - 1), y, points, cost));
            if (!avilpoints.Any(a => a.X == x && a.Y == TileMapUtil.CalculateY(y - 1)))
                avilpoints.AddRange(CheckTile(x, TileMapUtil.CalculateY(y - 1), points, cost));
            if (!avilpoints.Any(a => a.X == x && a.Y == TileMapUtil.CalculateY(y + 1)))
                avilpoints.AddRange(CheckTile(x, TileMapUtil.CalculateY(y + 1), points, cost));
            
            return avilpoints;
        }
    }
}
