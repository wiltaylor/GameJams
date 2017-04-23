using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.View.TileMap;
using Assets.Systems.CommandManager;
using Assets.Systems.PlayerManager;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;
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
    private readonly List<UnitView> _units = new List<UnitView>();

    private void StartUpCamera()
    {
        var building = TileMapService.Instance.Map.Buildings.First(b => b.PlayerOwned);
        PlayerService.Instance.CentreCameraAtTile(building.X, building.Y);
    }

	private void Start ()
	{
	    CommandService.Instance.HighlightChanged += InstanceOnHighlightChanged;

        var tilesettings = TileSet.Tiles.Select(t => t.TileSettings).ToArray();
	    var buildingsettings = TileSet.Buildings.Select(b => b.BuildingSettings).ToArray();
	    TileMapService.Instance.NewMap(GeneratorSettings, tilesettings, buildingsettings);
	    
        _map = TileMapService.Instance.Map;
        _map.TileChanged += _map_TileChanged;

        UnitService.Instance.AssignUnitDefinition(TileSet.Units.Select(t => t.UnitSettings).ToArray());
        UnitService.Instance.UnitChanged += UnitChanged;
        
        PlayerService.Instance.GenerateStartPosition();

        PlayerService.Instance.BeforeCameraCentre += Instance_BeforeCameraCentre;

	    Invoke("StartUpCamera", 0.25f);

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

    private void Instance_BeforeCameraCentre(object sender, CameraEventArgs e)
    {
        var obj = _tiles.FirstOrDefault(t => t.X == e.X && t.Y == e.Y);
        if (obj == null)
            return;

        e.ObjectToCentreOn = obj.gameObject;
    }

    private void InstanceOnHighlightChanged(object sender, EventArgs eventArgs)
    {
        foreach(var t in _tiles)
            t.Highlight(false);

        if (CommandService.Instance.UnitMoveRange == null) return; 
        
        var hviews = CommandService.Instance.UnitMoveRange
            .Select(c => _tiles.FirstOrDefault(t => t.X == c.X && t.Y == c.Y))
            .Where(v => v != null);

        foreach (var h in hviews)
        {
            h.Highlight(true);
        }
    }

    private void UnitChanged(object sender, UnitEventArgs unitEventArgs)
    {
        var unit = unitEventArgs.ChangedUnit;

        var view = _units.FirstOrDefault(u => u.Id == unit.UnitId);

        if (view == null)
        {
            var prefab = TileSet.Units.First(u => u.UnitSettings.Type == unit.Type).Prefab;
            var obj = Instantiate(prefab, transform);
            view = obj.GetComponent<UnitView>();

            _units.Add(view);

            view.X = unit.X;
            view.Y = unit.Y;
            view.Id = unit.UnitId;

            obj.transform.position = new Vector3(unit.X * (TilePixelWidth * 0.01f), unit.Y * (TilePixelHeight * 0.01f));
            obj.SetActive(true);
        }
        else
        {
            view.transform.position = new Vector3(unit.X * (TilePixelWidth * 0.01f), unit.Y * (TilePixelHeight * 0.01f));
        }

        if(unit.Hp <= 0)
            view.Destruct();
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
            building.Destruct();
    }
}
