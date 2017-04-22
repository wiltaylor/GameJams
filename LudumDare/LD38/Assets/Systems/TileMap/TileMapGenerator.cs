using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Assets.Systems.TileMap
{
    public class TileMapGenerator
    {
        public TileMap Generate(TileMapGeneratorSettings tileMapGeneratorSettings, IList<TileSettings> tileSettings, IList<BuildingSetting> buildingSettings)
        {
            var map = new TileMap(tileMapGeneratorSettings.Width, tileMapGeneratorSettings.Height,
                tileMapGeneratorSettings.FillType, tileSettings, buildingSettings)
            {
                TileDamageAmmount = tileMapGeneratorSettings.TileDamageAmmount,
                TileDamageRange = tileMapGeneratorSettings.TileDamageRange
            };

            GenerateTileMap(map, tileMapGeneratorSettings, tileSettings);
            GenerateBuildings(map, tileMapGeneratorSettings.Buildings, buildingSettings);

            return map;
        }

        private void GenerateBuildings(TileMap map, IEnumerable<BuildingGeneratorSetting> buildingGeneratorSettings, IList<BuildingSetting> buildingSettings)
        {
            foreach (var building in buildingGeneratorSettings)
            {
                var qty = Random.Range(building.MinQty, building.MaxQty);
                var x = 0;
                var y = 0;

                for (var q = 0; q < qty; q++)
                {
                    while (true)
                    {
                        x = Random.Range(0, map.MapWidth - 1);
                        y = Random.Range(0, map.MapHeight - 1);
                        var settings = buildingSettings.First(b => b.Type == building.Type);
                        var tile = map.MapData[x, y];

                        //Check correct tile type
                        if(!settings.AllowedToBuildOn.Any(a => a == tile.TileType))
                            continue;
                        
                        //check distance
                        var bTop = y - settings.BuildingDistance;
                        var bLeft = x - settings.BuildingDistance;
                        var bBottom = y + settings.BuildingDistance;
                        var bRight = x + settings.BuildingDistance;

                        var ruleCheckFail = false;

                        for (var bx = bLeft; bx <= bRight; bx++)
                        {
                            var targetX = bx;

                            if (targetX < 0)
                                targetX += map.MapWidth;

                            if (targetX >= map.MapWidth)
                                targetX -= map.MapWidth;

                            for (var by = bTop; by <= bBottom; by++)
                            {
                                var targetY = by;

                                if (targetY < 0)
                                    targetY += map.MapHeight;

                                if (targetY >= map.MapHeight)
                                    targetY -= map.MapHeight;

                                if (map.GetBuildingAt(targetX, targetY) != null)
                                    ruleCheckFail = true;
                            }
                        }

                        if(ruleCheckFail)
                            continue;

                        break;
                    }

                    var newbuilding = map.SetBuilding(x, y, building.Type);

                    if (newbuilding.Type == BuildingType.City)
                    {
                        newbuilding.Population = Random.Range(building.MinCityPopulation, building.MaxCityPopulation);
                    }
                }
            }
        }

        private void GenerateTileMap(TileMap map, TileMapGeneratorSettings tileMapGeneratorSettings, IList<TileSettings> tileSettings)
        {
            foreach (var island in tileMapGeneratorSettings.Islands)
            {
                for (var qty = 0; qty < island.Qty; qty++)
                {
                    var islandWidth = Random.Range(island.MinWidth, island.MaxHeight);
                    var islandHeight = Random.Range(island.MinHeight, island.MinHeight);

                    var left = Random.Range(0, map.MapWidth - 1);
                    var top = Random.Range(0, map.MapHeight - 1);

                    for (var x = 0; x < islandWidth; x++)
                    {
                        var targetX = x;
                        if (left + targetX >= map.MapWidth)
                            targetX -= map.MapWidth;

                        for (var y = 0; y < islandHeight; y++)
                        {
                            var targetY = y;

                            if (top + targetY >= map.MapHeight)
                                targetY -= map.MapHeight;


                            map.SetTile(left + targetX, top + targetY, island.Tile);
                        }
                    }
                }
            }

            for (var i = 0; i < tileMapGeneratorSettings.MutateCycles; i++)
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

                if (targetX >= map.MapWidth)
                    targetX -= map.MapWidth;

                if (targetY >= map.MapHeight)
                    targetY -= map.MapHeight;

                if (targetX < 0)
                    targetX += map.MapWidth;

                if (targetY < 0)
                    targetY += map.MapHeight;

                map.SetTile(targetX, targetY, tile);
            }
        }
    }
}
