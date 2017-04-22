using System;

namespace Assets.Systems.TileMap
{
    [Serializable]
    public class BuildingGeneratorSetting
    {
        public BuildingType Type;
        public int MaxQty;
        public int MinQty;
        public int MaxCityPopulation;
        public int MinCityPopulation;
    }
}
