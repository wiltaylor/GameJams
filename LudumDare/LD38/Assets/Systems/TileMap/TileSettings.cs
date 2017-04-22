using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.WSA;

namespace Assets.Systems.TileMap
{
    [CreateAssetMenu(fileName = "MapGeneratorSettings", menuName = "MapGenerator/TileData", order = 1)]
    public class TileSettings : ScriptableObject
    {
        public TileType TileId;
        public float StartHp;
        public float DecayRate;
        public int MoveCost;
    }
}
