using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Systems.TileMap
{
    public class TileMap
    {
        public int MapHeight { get; private set; }
        public int MapWidth { get; private set; }
        public Tile[,] MapData { get; set; }
        public event Action<TileMap> MapRefresh;
        public readonly IList<TileSettings> TileSettings;
        public int TileDamageRange { get; set; }
        public float TileDamageAmmount { get; set; }

        public TileMap(int width, int height, TileType fillTile, IList<TileSettings> tilesettings)
        {
            MapRefresh += tm => { };
            MapWidth = width;
            MapHeight = height;
            MapData = new Tile[MapWidth, MapHeight];
            TileSettings = tilesettings;

            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    SetTile(x, y, fillTile);
                }
            }
        }

        public void SetTile(int x, int y, TileType type)
        {
            if (MapData[x, y] == null)
                MapData[x, y] = new Tile(x, y);

            var tileDefaults = TileSettings.First(t => t.TileId == type);

            MapData[x, y].StartHp = tileDefaults.StartHp;
            MapData[x, y].Hp = tileDefaults.StartHp;
            MapData[x, y].DecayRate = tileDefaults.DecayRate;
            MapData[x, y].MoveCost = tileDefaults.MoveCost;
        }

        public void MapUpdate()
        {
            if (MapRefresh != null)
                MapRefresh(this);
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
    }
}