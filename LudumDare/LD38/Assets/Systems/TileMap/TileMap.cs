using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Systems.PlayerManager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Systems.TileMap
{
    public class TileMap
    {
        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }
        public Tile[,] MapData { get; set; }
        public event EventHandler<TileMapEventAargs> TileChanged = (sender, args) => { };
        public event EventHandler<BuildingEventArgs> BuildingChange = (sender, args) => { };

        public readonly IList<TileSettings> TileSettings;
        public readonly IList<BuildingSetting> BuildingSettings;
        public int TileDamageRange { get; set; }
        public float TileDamageAmmount { get; set; }

        public List<Building> Buildings { get; set; }

        public int TotalTiles { get; private set; }
        public int VoidTiles { get; private set; }

        public float TileDamagePerTurn { get; set; }
        
        public TileMap(int width, int height, TileType fillTile, IList<TileSettings> tilesettings, IList<BuildingSetting> buildingSettings)
        {
            TileChanged += (sender, aargs) => { };
            MapWidth = width;
            MapHeight = height;
            MapData = new Tile[MapWidth, MapHeight];
            TileSettings = tilesettings;
            BuildingSettings = buildingSettings;
            Buildings = new List<Building>();

            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    SetTile(x, y, fillTile);
                }
            }
        }

        public void MineBuilding(int x, int y)
        {
            var building = GetBuildingAt(x, y);

            building.PlayerOwned = true;

            PlayerService.Instance.Iron += building.IronPerOwn;
            PlayerService.Instance.FaithPerTurn += building.FaithPerTurn;
            PlayerService.Instance.TotalHuamns += building.HumanPerOwn;

            BuildingChange(this, new BuildingEventArgs{Building = building});
        }

        public Building GetBuildingAt(int x, int y)
        {
            return Buildings.FirstOrDefault(b => b.X == x && b.Y == y);
        }

        public void SetTile(int x, int y, TileType type)
        {

            if(x >= MapWidth)
                Debug.Log("bad x");

            if (y >= MapHeight)
                Debug.Log("bad y");

            if (MapData[x, y] == null)
            {
                TotalTiles++;
                MapData[x, y] = new Tile(x, y);
            }

            if(TileSettings == null)
                Debug.Log("foo");

            var tileDefaults = TileSettings.First(t => t.TileId == type);

            if (type == TileType.Void)
                VoidTiles++;

            MapData[x, y].StartHp = Random.Range(tileDefaults.MinStartHp, tileDefaults.MaxStartHp);
            MapData[x, y].Hp = MapData[x, y].StartHp;
            MapData[x, y].DecayRate = tileDefaults.DecayRate;
            MapData[x, y].MoveCost = tileDefaults.MoveCost;
            MapData[x, y].TileType = type;
            MapData[x, y].Passable = tileDefaults.Passable;
        }

        public Building SetBuilding(int x, int y, BuildingType type)
        {
            var settings = BuildingSettings.First(b => b.Type == type);

            var building = new Building
            {
                Type = type,
                PlayerOwned = false,
                Population = 0,
                X = x,
                Y = y,
                FaithPerTurn = settings.FaithPerTurn,
                IronPerOwn = settings.IronPerOwn,
                HumanPerOwn = settings.HumanPerOwn,
                MaxHp = Random.Range(settings.MinHp, settings.MaxHp)
            };

            building.Hp = building.MaxHp;

            Buildings.Add(building);

            var dmg = Random.Range(settings.MinHpModifier, settings.MaxHpModifier) * -1;
            DamageTile(x, y, settings.HpModifierRange, dmg);

            return building;
        }

        public void DamageTile(int x, int y, float ammount)
        {
            var tile = MapData[x, y];

            if (tile.TileType == TileType.Void)
                return;

            tile.Hp -= tile.DecayRate * ammount;

            if (tile.Hp > 0f)
                return;

            SetTile(x,y, TileType.Void);

            DamageTile(x, y, TileDamageRange, TileDamageAmmount);

            TileChanged(this, new TileMapEventAargs(x, y));
        }

        public void DamageTile(int x, int y, int range, float ammount)
        {
            var left = x - range;
            var top = y - range;
            var right = x + range;
            var bottom = y + range;

            for (var tx = left; tx <= right; tx++)
            {
                for (var ty = top; ty <= bottom; ty++)
                {
                    var targetx = tx;
                    var targety = ty;

                    if (targety < 0)
                        targety += MapHeight;
                    if (targety >= MapHeight)
                        targety -= MapHeight;

                    if (targetx < 0)
                        targetx += MapWidth;
                    if (targetx >= MapWidth)
                        targetx -= MapWidth;

                    DamageTile(targetx, targety, ammount);
                }
            }

        }

        public void DamageMap(float ammount)
        {
            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    DamageTile(x, y, ammount);
                }
            }
        }

        public void TurnRefresh()
        {
            foreach (var b in Buildings)
                b.HasBuiltThisTurn = false;

            DamageMap(TileDamagePerTurn);
        }
    }
}