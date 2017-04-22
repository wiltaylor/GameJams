using System;
using Assets.Systems.TileMap;
using UnityEngine;

namespace Assets.Scripts.View.TileMap
{
    [CreateAssetMenu(fileName = "TileSet", menuName = "MapGenerator/TileSet", order = 1)]
    public class TileSet : ScriptableObject
    {
        public TilePrefab[] Tiles;
    }

    [Serializable]
    public class TilePrefab
    {
        public TileType Type;
        public GameObject Prefab;
        public TileSettings TileSettings;
    }
}
