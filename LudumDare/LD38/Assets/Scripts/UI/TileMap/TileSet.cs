using System;
using Assets.Systems.TileMap;
using Assets.Systems.Unit;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Assets.Scripts.View.TileMap
{
    [CreateAssetMenu(fileName = "TileSet", menuName = "MapGenerator/TileSet", order = 1)]
    public class TileSet : ScriptableObject
    {
        public TilePrefab[] Tiles;
        public BuildingPrefab[] Buildings;
        public UnitPrefab[] Units;
    }

    [Serializable]
    public class TilePrefab
    {
        public TileType Type;
        public GameObject Prefab;
        public TileSettings TileSettings;
    }

    [Serializable]
    public class BuildingPrefab
    {
        public BuildingType Type;
        public GameObject Prefab;
        public BuildingSetting BuildingSettings;
    }

    [Serializable]
    public class UnitPrefab
    {
        public UnitType Unit;
        public GameObject Prefab;
        public UnitSettings UnitSettings;
    }
}
