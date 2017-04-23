namespace Assets.Systems.TileMap
{
    public class Tile
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public float StartHp { get; set; }
        public float Hp { get; set; }
        public float DecayRate { get; set; }
        public int MoveCost { get; set; }
        public TileType TileType { get; set; }
        public bool Visable { get; set; }
        public bool Passable { get; set; }

        
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
