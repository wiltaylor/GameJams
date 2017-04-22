using System.Collections.Generic;

namespace Assets.Systems.TileMap
{
    public class TileMapService
    {
        private static TileMapService _instance;

        public static TileMapService Instance
        {
            get { return _instance ?? (_instance = new TileMapService()); }
        }

        public void NewMap(TileMapGeneratorSettings genSettings, IList<TileSettings> tilesettings)
        {
            var generator = new TileMapGenerator();
            Map = generator.Generate(genSettings, tilesettings);
        }

        public TileMap Map { get; private set; }
    }
}
