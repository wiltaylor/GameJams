using System;
using System.Linq;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;

namespace Assets.Systems.PlayerManager
{
    public class PlayerService
    {
        private static PlayerService _instance;

        public event EventHandler<CameraEventArgs> BeforeCameraCentre = (sender, args) => { };
        public event EventHandler<CameraEventArgs> CameraCentre = (sender, args) => { };
        public int Iron;
        public int IronUsed;
        public int Faith;
        public int TotalHumans;
        public int UsedHumans;
        public int FaithPerTurn;

        public static PlayerService Instance
        {
            get { return _instance ?? (_instance = new PlayerService()); }
        }

        public void GenerateStartPosition()
        {
            var building = TileMapService.Instance.Map.Buildings.First(b => b.Type == BuildingType.City);
            building.PlayerOwned = true;
            FaithPerTurn += building.FaithPerTurn;
            TotalHumans += building.HumanPerOwn;
            Iron += building.IronPerOwn;

            var unit = UnitService.Instance.AddUnit(building.X, building.Y, UnitType.Scout, UnitFaction.Player, true);
        }

        public void CentreCameraAtTile(int x, int y)
        {
            var args = new CameraEventArgs
            {
                X = x,
                Y = y
            };

            BeforeCameraCentre(this, args);
            CameraCentre(this, args);

        }

        public void IncrementResources()
        {
            Faith += FaithPerTurn;
        }
    }
}
