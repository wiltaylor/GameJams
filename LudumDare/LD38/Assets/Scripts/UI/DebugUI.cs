using System.Collections;
using System.Collections.Generic;
using Assets.Systems.TileMap;
using UnityEngine;

public class DebugUI : MonoBehaviour {

    public void RevealMap()
    {
        var map = TileMapService.Instance.Map;

        for (var x = 0; x < map.MapWidth; x++)
        {
            for (var y = 0; y < map.MapHeight; y++)
            {
                map.MapData[x, y].Visable = true;
            }
        }

        map.RefreshMap();
    }
}
