using System.Linq;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;

namespace Assets.Systems.PlayerManager
{
    public class PlayerService
    {
        private static PlayerService _instance;

        public static PlayerService Instance
        {
            get { return _instance ?? (_instance = new PlayerService()); }
        }

        public void GenerateStartPosition()
        {
            var building = TileMapService.Instance.Map.Buildings.First(b => b.Type == BuildingType.City);
            building.PlayerOwned = true;

            UnitService.Instance.AddUnit(building.X, building.Y, UnitType.Scout, UnitFaction.Player);
        }
    }
}
