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

    private readonly List<TileView> _tiles = new List<TileView>();
    private readonly List<BuildingView> _buildings = new List<BuildingView>();

	private void Start ()
	{
	    var tilesettings = TileSet.Tiles.Select(t => t.TileSettings).ToArray();
	    var buildingsettings = TileSet.Buildings.Select(b => b.BuildingSettings).ToArray();
	    TileMapService.Instance.NewMap(GeneratorSettings, tilesettings, buildingsettings);
        _map = TileMapService.Instance.Map;
        _map.TileChanged += _map_TileChanged;

	    for (var x = 0; x < _map.MapWidth; x++)
	    {
	        for (var y = 0; y < _map.MapHeight; y++)
	        {
	            var tile = _map.MapData[x, y];
	            var prefab = TileSet.Tiles.First(t => t.Type == tile.TileType).Prefab;

                if(prefab == null)
                    continue;

	            var obj = Instantiate(prefab, transform);
	            var view = obj.GetComponent<TileView>();

	            view.MapView = this;
	            view.X = x;
	            view.Y = y;

                obj.transform.position = new Vector3(x * (TilePixelWidth * 0.01f), y * (TilePixelHeight * 0.01f));
                obj.SetActive(true);

	            _tiles.Add(view);

            }
        }

	    foreach (var building in _map.Buildings)
	    {
	        var prefab = TileSet.Buildings.First(b => b.Type == building.Type).Prefab;
	        var obj = Instantiate(prefab, transform);
	        var view = obj.GetComponent<BuildingView>();

	        view.MapView = this;
	        view.X = building.X;
	        view.Y = building.Y;
            
            obj.transform.position = new Vector3(building.X * (TilePixelWidth * 0.01f), building.Y * (TilePixelHeight * 0.01f));
	        obj.SetActive(true);

            _buildings.Add(view);
        }
	}

    private void _map_TileChanged(object sender, TileMapEventAargs e)
    {
        Debug.Log("Tile Changed: X - " + e.X + " Y - " + e.Y);
        var tile = _tiles.FirstOrDefault(t => t.X == e.X && t.Y == e.Y);
        var building = _buildings.FirstOrDefault(b => b.X == e.X && b.Y == e.Y);

        if (tile == null) return;
        if (_map.MapData[e.X, e.Y].TileType != TileType.Void) return;

        tile.Destruct();

        if(building != null)
            Destroy(building.gameObject);
    }
}
