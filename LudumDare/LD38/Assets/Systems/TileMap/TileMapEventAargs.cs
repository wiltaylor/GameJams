using System;

namespace Assets.Systems.TileMap
{
    public class TileMapEventAargs : EventArgs
    {
        public readonly int X;
        public readonly int Y;

        public TileMapEventAargs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
