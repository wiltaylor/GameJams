using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Systems.TileMap
{
    public class TileMapGenerator
    {
        public TileMap Generate(TileMapGeneratorSettings settings, IList<TileSettings> tileSettings)
        {
            var map = new TileMap(settings.Width, settings.Height, settings.FillType, tileSettings)
            {
                TileDamageAmmount =  settings.TileDamageAmmount,
                TileDamageRange =  settings.TileDamageRange
            };
            

            foreach (var island in settings.Islands)
            {
                var islandWidth = Random.Range(island.MinWidth, island.MaxHeight);
                var islandHeight = Random.Range(island.MinHeight, island.MinHeight);

                var left = Random.Range(0, map.MapWidth - 1);
                var top = Random.Range(0, map.MapHeight - 1);

                for (var x = 0; x < islandWidth; x++)
                {
                    if (left + x >= map.MapWidth)
                        break;
                    for (var y = 0; y < islandHeight; y++)
                    {
                        if (top + y >= map.MapHeight)
                            break;
                        
                        map.SetTile(left + x, top + y, island.Tile);
                    }
                }
            }

            for (var i = 0; i < settings.MutateCycles; i++)
            {
                var x = Random.Range(0, map.MapWidth - 1);
                var y = Random.Range(0, map.MapHeight - 1);
                var dir = Random.Range(0, 3);
                var tile = map.MapData[x, y].TileType;
                var targetX = 0;
                var targetY = 0;

                if (dir == 0)
                {
                    targetX = x;
                    targetY = y - 1;
                }
                else if (dir == 1)
                {
                    targetX = x + 1;
                    targetY = y;
                }
                else if (dir == 2)
                {
                    targetX = x;
                    targetY = y + 1;
                }
                else if (dir == 3)
                {
                    targetX = x - 1;
                    targetY = y;
                }

                if (targetX > map.MapWidth)
                    targetX -= map.MapWidth;

                if (targetY > map.MapHeight)
                    targetY -= map.MapHeight;

                map.SetTile(targetX, targetY, tile);
            }

            return map;

        }
    }
}
