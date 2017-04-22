using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.View.TileMap;
using Assets.Systems.TileMap;
using UnityEngine;

public class TileMapView : MonoBehaviour
{
    private TileMap _map;
    public TileMapGeneratorSettings GeneratorSettings;
    public TileSet TileSet;
    public int TilePixelWidth = 64;
    public int TilePixelHeight = 64;   

	private void Start ()
	{
	    var tilesettings = TileSet.Tiles.Select(t => t.TileSettings).ToArray();
	    var buildingsettings = TileSet.Buildings.Select(b => b.BuildingSettings).ToArray();
	    TileMapService.Instance.NewMap(GeneratorSettings, tilesettings, buildingsettings);
        _map = TileMapService.Instance.Map;

	    for (var x = 0; x < _map.MapWidth; x++)
	    {
	        for (var y = 0; y < _map.MapHeight; y++)
	        {
	            var tile = _map.MapData[x, y];
	            var prefab = TileSet.Tiles.First(t => t.Type == tile.TileType).Prefab;
	            var obj = Instantiate(prefab, transform);

                obj.transform.position = new Vector3(x * (TilePixelWidth * 0.01f), y * (TilePixelHeight * 0.01f));
                obj.SetActive(true);
	        }
        }

	    foreach (var building in _map.Buildings)
	    {
	        var prefab = TileSet.Buildings.First(b => b.Type == building.Type).Prefab;
	        var obj = Instantiate(prefab, transform);

	        obj.transform.position = new Vector3(building.X * (TilePixelWidth * 0.01f), building.Y * (TilePixelHeight * 0.01f));
	        obj.SetActive(true);
        }
	}
}
